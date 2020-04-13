﻿using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static Microsoft.Win32.Registry;

namespace GameEstate.Core
{
    /// <summary>
    /// FileManager
    /// </summary>
    public class FileManager
    {
        public static FileManager ParseFileManager(JObject obj)
        {
            var r = new FileManager { };
            // registry
            if (obj["registry"] != null)
                foreach (var x in obj["registry"].Cast<JProperty>())
                    foreach (var value in x.Value["key"] is JArray a ? a.Values().Select(y => (string)((JValue)y).Value) : new[] { (string)x.Value["key"] })
                    {
                        var path = value?.Replace('/', '\\');
                        path = GetExePath(Is64Bit ? $"Wow6432Node\\{path}" : path);
                        if (path != null && Directory.Exists(path))
                        {
                            var assets = (string)x.Value["assets"];
                            if (assets != null)
                                path = Path.Combine(path, assets);
                            if (Directory.Exists(path))
                            {
                                r.Locations.Add(x.Name, path);
                                break;
                            }
                        }
                    }
            // direct
            if (obj["direct"] != null)
                foreach (var x in obj["direct"].Cast<JProperty>())
                    foreach (var value in x.Value["path"] is JArray a ? a.Values().Select(y => (string)((JValue)y).Value) : new[] { (string)x.Value["path"] })
                    {
                        var path = value?.Replace('/', '\\');
                        if (path != null && Directory.Exists(path))
                        {
                            var assets = (string)x.Value["assets"];
                            if (assets != null)
                                path = Path.Combine(path, assets);
                            if (Directory.Exists(path))
                            {
                                r.Locations.Add(x.Name, path);
                                break;
                            }
                        }
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
        /// Gets a value indicating whether this instance is data present.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is data present; otherwise, <c>false</c>.
        /// </value>
        public bool IsDataPresent => Locations.Count != 0;

        /// <summary>
        /// The locations
        /// </summary>
        public IDictionary<string, string> Locations = new Dictionary<string, string>();

        /// <summary>
        /// Gets the game file paths.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="pathOrPattern">The path or pattern.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">pathOrPattern</exception>
        /// <exception cref="ArgumentOutOfRangeException">pathOrPattern</exception>
        public string[] GetGameFilePaths(string game, string pathOrPattern)
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
        public string[] GetLocalFilePaths(string pathOrPattern, out bool streamPak)
        {
            if (pathOrPattern == null)
                throw new ArgumentNullException(nameof(pathOrPattern));
            var searchPattern = Path.GetFileName(pathOrPattern);
            var path = Path.GetDirectoryName(pathOrPattern);
            // file
            if (!string.IsNullOrEmpty(searchPattern))
            {
                streamPak = false;
                return searchPattern.Contains('*') ? Directory.GetFiles(path, searchPattern)
                    : File.Exists(pathOrPattern) ? new[] { pathOrPattern } : null;
            }
            // folder
            streamPak = true;
            searchPattern = Path.GetFileName(path);
            path = Path.GetDirectoryName(path);
            return pathOrPattern.Contains('*') ? Directory.GetDirectories(path, searchPattern)
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
        public string[] GetHttpFilePaths(Uri uri, out Uri host, out bool streamPak)
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
        /// <param name="subName">Name of the sub.</param>
        /// <returns></returns>
        protected static string GetExePath(string subName)
        {
            try
            {
                var key = new Func<RegistryKey>[] {
                    () => LocalMachine.OpenSubKey($"SOFTWARE\\{subName}"),
                    () => CurrentUser.OpenSubKey($"SOFTWARE\\{subName}"),
                    () => ClassesRoot.OpenSubKey($"VirtualStore\\MACHINE\\SOFTWARE\\{subName}")
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
