using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;
using static Microsoft.Win32.Registry;

namespace GameEstate.Core
{
    /// <summary>
    /// FileManager
    /// </summary>
    public class FileManager : AbstractFileManager
    {
        public static void WithTmpFile(string body, Action<string> action)
        {
            string fileName = null;
            try
            {
                fileName = Path.GetTempFileName();
                var fileInfo = new FileInfo(fileName)
                {
                    Attributes = FileAttributes.Temporary
                };
                using (var s = fileInfo.AppendText())
                    s.Write(body);
                action(fileName);
            }
            finally
            {
                if (fileName != null && File.Exists(fileName))
                    File.Delete(fileName);
            }
        }

        static string PathWithSpecialFolders(string path, string rootPath = null) =>
            path.StartsWith("%Path%", StringComparison.OrdinalIgnoreCase) ? $"{rootPath}{path.Substring(6)}"
            : path.StartsWith("%AppData%", StringComparison.OrdinalIgnoreCase) ? $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}{path.Substring(9)}"
            : path.StartsWith("%LocalAppData%", StringComparison.OrdinalIgnoreCase) ? $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}{path.Substring(14)}"
            : path;

        public static FileManager ParseFileManager(JsonElement elem)
        {
            bool TryGetSingleFileValue(string path, string ext, string select, out string value)
            {
                value = null;
                if (!File.Exists(path))
                    return false;
                var content = File.ReadAllText(path);
                value = ext switch
                {
                    "xml" => XDocument.Parse(content).XPathSelectElement(select)?.Value,
                    _ => throw new ArgumentOutOfRangeException(nameof(ext)),
                };
                if (value != null)
                    value = Path.GetDirectoryName(value);
                return true;
            }
            bool TryGetRegistryByKey(string key, JsonProperty prop, JsonElement? keyElem, out string path)
            {
                path = GetRegistryExePath(Is64Bit ? $"Wow6432Node\\{key}" : key);
                if (keyElem == null)
                    return path != null;
                if (keyElem.Value.TryGetProperty("xml", out var xml)
                    && keyElem.Value.TryGetProperty("xmlPath", out var xmlPath)
                    && TryGetSingleFileValue(PathWithSpecialFolders(xml.GetString(), path), "xml", xmlPath.GetString(), out path))
                    return path != null;
                return false;
            }
            var r = new FileManager { };
            bool TryAddPath(string path, JsonProperty prop)
            {
                if (path == null || !Directory.Exists(path = PathWithSpecialFolders(path)))
                    return false;
                path = prop.Value.TryGetProperty("assets", out var z) ? Path.Combine(path, z.GetString()) : path;
                if (Directory.Exists(path))
                {
                    r.Locations.Add(prop.Name, path.Replace('/', '\\'));
                    return true;
                }
                return false;
            }

            // registry
            if (elem.TryGetProperty("registry", out var z))
                foreach (var prop in z.EnumerateObject())
                {
                    var keys = prop.Value.TryGetProperty("key", out z) ? z.ValueKind switch
                    {
                        JsonValueKind.String => new[] { z.GetString() },
                        JsonValueKind.Array => z.EnumerateArray().Select(y => y.GetString()),
                        _ => throw new ArgumentOutOfRangeException(),
                    } : null;
                    if (keys != null)
                        foreach (var key in keys)
                            if (TryGetRegistryByKey(key, prop, prop.Value.TryGetProperty(key, out z) ? (JsonElement?)z : null, out var path)
                                && TryAddPath(path, prop))
                                break;
                }

            // direct
            if (elem.TryGetProperty("direct", out z))
                foreach (var prop in z.EnumerateObject())
                {
                    var paths = prop.Value.TryGetProperty("path", out z) ? z.ValueKind switch
                    {
                        JsonValueKind.String => new[] { z.GetString() },
                        JsonValueKind.Array => z.EnumerateArray().Select(y => y.GetString()),
                        _ => throw new ArgumentOutOfRangeException(),
                    } : null;
                    if (paths != null)
                        foreach (var path in paths)
                            if (TryAddPath(path, prop))
                                break;
                }
            return r;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [is64 bit].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is64 bit]; otherwise, <c>false</c>.
        /// </value>
        public static bool Is64Bit { get; set; } = true;

        /// <summary>
        /// Gets the host factory.
        /// </summary>
        /// <value>
        /// The host factory.
        /// </value>
        public override Func<Uri, string, AbstractHost> HostFactory => HttpHost.Factory;

        /// <summary>
        /// Parses the resource.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">fragment</exception>
        /// <exception cref="InvalidOperationException">
        /// No {gameName} resources match.
        /// or
        /// No {gameName} resources match.
        /// or
        /// No {gameName} resources match.
        /// </exception>
        public override Estate.Resource ParseResource(Estate estate, Uri uri)
        {
            var fragment = uri.Fragment?.Substring(uri.Fragment.Length != 0 ? 1 : 0);
            var game = estate.GetGame(fragment);
            var r = new Estate.Resource { Game = game.id };
            // file-scheme
            if (string.Equals(uri.Scheme, "game", StringComparison.OrdinalIgnoreCase))
                r.Paths = GetGameFilePaths(r.Game, uri.LocalPath.Substring(1)) ?? throw new InvalidOperationException($"No {game.id} resources match.");
            // file-scheme
            else if (uri.IsFile)
                r.Paths = GetLocalFilePaths(uri.LocalPath, out r.StreamPak) ?? throw new InvalidOperationException($"No {game.id} resources match.");
            // network-scheme
            else
                r.Paths = GetHttpFilePaths(uri, out r.Host, out r.StreamPak) ?? throw new InvalidOperationException($"No {game.id} resources match.");
            return r;
        }

        /// <summary>
        /// Gets the game file paths.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="pathOrPattern">The path or pattern.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">pathOrPattern</exception>
        /// <exception cref="ArgumentOutOfRangeException">pathOrPattern</exception>
        public override string[] GetGameFilePaths(string game, string pathOrPattern)
        {
            if (pathOrPattern == null)
                throw new ArgumentNullException(nameof(pathOrPattern));
            var searchPattern = Path.GetFileName(pathOrPattern);
            // folder
            if (string.IsNullOrEmpty(searchPattern))
                throw new ArgumentOutOfRangeException(nameof(pathOrPattern), pathOrPattern);
            // file
            return Locations.TryGetValue(game, out var path)
                ? pathOrPattern.Contains('*') ? Directory.GetFiles(path, pathOrPattern) : new[] { Path.Combine(path, pathOrPattern) }
                : null;
        }

        /// <summary>
        /// Gets the local file paths.
        /// </summary>
        /// <param name="pathOrPattern">The path or pattern.</param>
        /// <param name="streamPak">if set to <c>true</c> [file pak].</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">pathOrPattern</exception>
        public override string[] GetLocalFilePaths(string pathOrPattern, out bool streamPak)
        {
            if (pathOrPattern == null)
                throw new ArgumentNullException(nameof(pathOrPattern));
            var searchPattern = Path.GetFileName(pathOrPattern);
            var path = Path.GetDirectoryName(pathOrPattern);
            // file
            if (!string.IsNullOrEmpty(searchPattern))
            {
                streamPak = false;
                return searchPattern.Contains('*')
                    ? Directory.GetFiles(path, searchPattern)
                    : File.Exists(pathOrPattern) ? new[] { pathOrPattern } : null;
            }
            // folder
            streamPak = true;
            searchPattern = Path.GetFileName(path);
            path = Path.GetDirectoryName(path);
            return pathOrPattern.Contains('*')
                ? Directory.GetDirectories(path, searchPattern)
                : Directory.Exists(pathOrPattern) ? new[] { pathOrPattern } : null;
        }

        /// <summary>
        /// Gets the host file paths.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="host">The host.</param>
        /// <param name="streamPak">if set to <c>true</c> [file pak].</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">uri</exception>
        /// <exception cref="ArgumentOutOfRangeException">pathOrPattern</exception>
        /// <exception cref="NotSupportedException">Web wildcard access to supported</exception>
        public override string[] GetHttpFilePaths(Uri uri, out Uri host, out bool streamPak)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            var pathOrPattern = uri.LocalPath;
            var searchPattern = Path.GetFileName(pathOrPattern);
            var path = Path.GetDirectoryName(pathOrPattern);
            // file
            if (!string.IsNullOrEmpty(searchPattern))
                throw new ArgumentOutOfRangeException(nameof(pathOrPattern), pathOrPattern); //: Web single file access to supported.
            // folder
            streamPak = true;
            searchPattern = Path.GetFileName(path);
            path = Path.GetDirectoryName(path);
            if (path.Contains('*'))
                throw new NotSupportedException("Web wildcard folder access");
            host = new UriBuilder(uri)
            {
                Path = $"{path}/",
                Fragment = null,
            }.Uri;
            if (searchPattern.Contains('*'))
            {
                var set = new HttpHost(host).GetSetAsync().Result ?? throw new NotSupportedException(".set not found. Web wildcard access");
                var pattern = $"^{Regex.Escape(searchPattern.Replace('*', '%')).Replace("_", ".").Replace("%", ".*")}$";
                return set.Where(x => Regex.IsMatch(x, pattern)).ToArray();
            }
            return new[] { searchPattern };
        }

        /// <summary>
        /// Gets the executable path.
        /// </summary>
        /// <param name="name">Name of the sub.</param>
        /// <returns></returns>
        protected static string GetRegistryExePath(string name)
        {
            try
            {
                name = name.Replace('/', '\\');
                var key = new Func<RegistryKey>[] {
                    () => LocalMachine.OpenSubKey($"SOFTWARE\\{name}"),
                    () => CurrentUser.OpenSubKey($"SOFTWARE\\{name}"),
                    () => ClassesRoot.OpenSubKey($"VirtualStore\\MACHINE\\SOFTWARE\\{name}")
                }.Select(x => x()).FirstOrDefault(x => x != null);
                if (key == null)
                    return null;
                var path = new[] { "Path", "Install Dir", "InstallDir" }.Select(x => key.GetValue(x) as string).FirstOrDefault(x => !string.IsNullOrEmpty(x) || Directory.Exists(x));
                if (path == null)
                {
                    path = new[] { "Installed Path", "ExePath", "Exe" }.Select(x => key.GetValue(x) as string).FirstOrDefault(x => !string.IsNullOrEmpty(x) || File.Exists(x));
                    if (path != null)
                        path = Path.GetDirectoryName(path);
                }
                return path != null && Directory.Exists(path) ? path : null;
            }
            catch { return null; }
        }
    }
}

