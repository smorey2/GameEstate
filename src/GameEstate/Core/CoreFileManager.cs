using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Microsoft.Win32.Registry;

namespace GameEstate.Core
{
    public abstract class CoreFileManager
    {
        public bool Is64Bit { get; set; } = true;
        public bool IsDataPresent => _locations.Count != 0;
        protected Dictionary<int, string> _locations = new Dictionary<int, string>();

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
                        _locations.Add(game, path);
                        if (many == false)
                            return;
                    }
                }
            }
        }

        public string[] GetFilePaths(string pathOrPattern, int game, bool many) =>
            _locations.TryGetValue(game, out var path) ? many
                ? Directory.GetFiles(path, pathOrPattern ?? throw new ArgumentNullException(nameof(pathOrPattern)))
                : new[] { Path.Combine(path, pathOrPattern ?? throw new ArgumentNullException(nameof(pathOrPattern))) }
                : null;

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

