using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Withes the temporary file.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <param name="action">The action.</param>
        [Obsolete]
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

        /// <summary>
        /// Gets the host factory.
        /// </summary>
        /// <value>
        /// The host factory.
        /// </value>
        public override Func<Uri, string, AbstractHost> HostFactory => HttpHost.Factory;

        #region Parse Resource

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
            return Locations.TryGetValue(game, out var path) ? ExpandAndSearchPaths(path, pathOrPattern).ToArray() : null;
        }

        static IEnumerable<string> ExpandAndSearchPaths(string path, string pathOrPattern)
        {
            // expand
            int expandStartIdx, expandMidIdx, expandEndIdx;
            if ((expandStartIdx = pathOrPattern.IndexOf('(')) != -1 &&
                (expandMidIdx = pathOrPattern.IndexOf(':', expandStartIdx)) != -1 &&
                (expandEndIdx = pathOrPattern.IndexOf(')', expandMidIdx)) != -1 &&
                expandStartIdx < expandEndIdx)
            {
                foreach (var expand in pathOrPattern.Substring(expandStartIdx + 1, expandEndIdx - expandStartIdx - 1).Split(':'))
                    foreach (var found in ExpandAndSearchPaths(path, pathOrPattern.Remove(expandStartIdx, expandEndIdx - expandStartIdx + 1).Insert(expandStartIdx, expand)))
                        yield return found;
                yield break;
            }
            // directory
            var directoryPattern = Path.GetDirectoryName(pathOrPattern);
            if (directoryPattern.IndexOf('*') != -1)
            {
                foreach (var directory in Directory.GetDirectories(path, directoryPattern))
                    foreach (var found in ExpandAndSearchPaths(directory, Path.GetFileName(pathOrPattern)))
                        yield return found;
                yield break;
            }
            // file
            var searchIdx = pathOrPattern.IndexOf('*');
            if (searchIdx == -1)
                yield return Path.Combine(path, pathOrPattern);
            else foreach (var file in Directory.GetFiles(path, pathOrPattern))
                    yield return file;
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

        #endregion

        #region Parse File-Manager

        public static FileManager ParseFileManager(JsonElement elem)
        {
            bool TryGetSingleFileValue(string path, string ext, string select, out string value)
            {
                value = null;
                if (!File.Exists(path))
                    return false;
                var content = File.ReadAllText(path);
                switch (ext)
                {
                    case "xml": value = XDocument.Parse(content).XPathSelectElement(select)?.Value; break;
                    default: throw new ArgumentOutOfRangeException(nameof(ext));
                }
                if (value != null)
                    value = Path.GetDirectoryName(value);
                return true;
            }
            bool TryGetRegistryByKey(string key, JsonProperty prop, JsonElement? keyElem, out string path)
            {
                path = GetRegistryExePath(new[] { $@"Wow6432Node\{key}", key });
                if (keyElem == null)
                    return !string.IsNullOrEmpty(path);
                if (keyElem.Value.TryGetProperty("path", out var path2))
                {
                    path = Path.GetFullPath(PathWithSpecialFolders(path2.GetString(), path));
                    return !string.IsNullOrEmpty(path);
                }
                else if (keyElem.Value.TryGetProperty("xml", out var xml)
                    && keyElem.Value.TryGetProperty("xmlPath", out var xmlPath)
                    && TryGetSingleFileValue(PathWithSpecialFolders(xml.GetString(), path), "xml", xmlPath.GetString(), out path))
                    return !string.IsNullOrEmpty(path);
                return false;
            }

            // parse
            var r = new FileManager { };
            bool TryAddPath(string path, JsonProperty prop)
            {
                if (path == null || !Directory.Exists(path = PathWithSpecialFolders(path)))
                    return false;
                path = Path.GetFullPath(path);
                path = prop.Value.TryGetProperty("assets", out var z2) ? Path.Combine(path, z2.GetString()) : path;
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
                    if (prop.Value.TryGetProperty("key", out z))
                    {
                        IEnumerable<string> keys;
                        switch (z.ValueKind)
                        {
                            case JsonValueKind.String: keys = new[] { z.GetString() }; break;
                            case JsonValueKind.Array: keys = z.EnumerateArray().Select(y => y.GetString()); break;
                            default: throw new ArgumentOutOfRangeException();
                        }
                        foreach (var key in keys)
                            if (TryGetRegistryByKey(key, prop, prop.Value.TryGetProperty(key, out z) ? (JsonElement?)z : null, out var path)
                                && TryAddPath(path, prop))
                                break;

                    }

            // direct
            if (elem.TryGetProperty("direct", out z))
                foreach (var prop in z.EnumerateObject())
                    if (prop.Value.TryGetProperty("path", out z))
                    {
                        IEnumerable<string> paths;
                        switch (z.ValueKind)
                        {
                            case JsonValueKind.String: paths = new[] { z.GetString() }; break;
                            case JsonValueKind.Array: paths = z.EnumerateArray().Select(y => y.GetString()); break;
                            default: throw new ArgumentOutOfRangeException();
                        }
                        foreach (var path in paths)
                            if (TryAddPath(path, prop))
                                break;
                    }
            return r;
        }

        /// <summary>
        /// Gets the executable path.
        /// </summary>
        /// <param name="name">Name of the sub.</param>
        /// <returns></returns>
        protected static string GetRegistryExePath(string[] paths)
        {
            var localMachine64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            var currentUser64 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            foreach (var path in paths)
                try
                {
                    var key = path.Replace('/', '\\');
                    var foundKey = new Func<RegistryKey>[] {
                        () => localMachine64.OpenSubKey($"SOFTWARE\\{key}"),
                        () => currentUser64.OpenSubKey($"SOFTWARE\\{key}"),
                        () => ClassesRoot.OpenSubKey($"VirtualStore\\MACHINE\\SOFTWARE\\{key}") }
                        .Select(x => x()).FirstOrDefault(x => x != null);
                    if (foundKey == null)
                        continue;
                    var foundPath = new[] { "Path", "Install Dir", "InstallDir", "InstallLocation" }
                        .Select(x => foundKey.GetValue(x) as string)
                        .FirstOrDefault(x => !string.IsNullOrEmpty(x) || Directory.Exists(x));
                    if (foundPath == null)
                    {
                        foundPath = new[] { "Installed Path", "ExePath", "Exe" }
                            .Select(x => foundKey.GetValue(x) as string)
                            .FirstOrDefault(x => !string.IsNullOrEmpty(x) || File.Exists(x));
                        if (foundPath != null)
                            foundPath = Path.GetDirectoryName(foundPath);
                    }
                    if (foundPath != null && Directory.Exists(foundPath))
                        return foundPath;
                }
                catch { return null; }
            return null;
        }

        static string PathWithSpecialFolders(string path, string rootPath = null) =>
            path.StartsWith("%Path%", StringComparison.OrdinalIgnoreCase) ? $"{rootPath}{path.Substring(6)}"
            : path.StartsWith("%AppData%", StringComparison.OrdinalIgnoreCase) ? $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}{path.Substring(9)}"
            : path.StartsWith("%LocalAppData%", StringComparison.OrdinalIgnoreCase) ? $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}{path.Substring(14)}"
            : path;

        #endregion
    }
}

