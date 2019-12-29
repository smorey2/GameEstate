using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Microsoft.Win32.Registry;

namespace GameEstate.Core
{
    /// <summary>
    /// CoreFileManager
    /// </summary>
    public abstract class CoreFileManager
    {
        /// <summary>
        /// Gets or sets a value indicating whether [is64 bit].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is64 bit]; otherwise, <c>false</c>.
        /// </value>
        public bool Is64Bit { get; set; } = true;

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
        public IDictionary<int, string> Locations = new Dictionary<int, string>();

        /// <summary>
        /// Loads from reg keys.
        /// </summary>
        /// <param name="regkeys">The regkeys.</param>
        /// <param name="subFolder">The sub folder.</param>
        /// <param name="many">The many.</param>
        protected void LoadFromRegKeys(IList<object> regkeys, Func<int, string> subFolder = null, bool? many = null)
        {
            for (var i = 0; i < regkeys.Count; i += 2)
            {
                var path = GetExePath(Is64Bit ? $"Wow6432Node\\{(string)regkeys[i]}" : (string)regkeys[i]);
                if (path != null && Directory.Exists(path))
                {
                    var game = (int)regkeys[i + 1];
                    if (subFolder != null)
                        path = Path.Combine(path, subFolder(game));
                    if (Directory.Exists(path))
                    {
                        Locations.Add(game, path);
                        if (many == false)
                            return;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the file paths.
        /// </summary>
        /// <param name="pathOrPattern">The path or pattern.</param>
        /// <param name="game">The game.</param>
        /// <param name="many">if set to <c>true</c> [many].</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// pathOrPattern
        /// or
        /// pathOrPattern
        /// </exception>
        public string[] GetFilePaths(string pathOrPattern, int game, bool many) =>
            Locations.TryGetValue(game, out var path) ? many
                ? Directory.GetFiles(path, pathOrPattern ?? throw new ArgumentNullException(nameof(pathOrPattern)))
                : new[] { Path.Combine(path, pathOrPattern ?? throw new ArgumentNullException(nameof(pathOrPattern))) }
                : null;

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

