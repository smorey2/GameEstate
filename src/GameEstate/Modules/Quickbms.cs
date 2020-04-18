using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameEstate.Modules
{
    public class Quickbms
    {
        readonly string _assetPath;

        public Quickbms(string assetPath)
        {
            _assetPath = assetPath ?? throw new ArgumentNullException(nameof(assetPath));
        }

        public static async Task<bool> ExtractPackageAsync(string packagePath, string scriptPath, string contentPath)
            => await Task.Run(() => ProcessLaunch(scriptPath, contentPath, packagePath));

        static bool ProcessLaunch(string bmsPath, string fname, string fdirectory, bool is4gb = true)
        {
            var fileName = Path.Combine("Modules", is4gb ? "quickbms_4gb_files.exe" : "quickbms.exe");
            try
            {
                var args = new List<string>(new[] { "-i" });
                //if (false)
                //    args.AddRange(new[] { "-f", "" });
                args.AddRange(new[] { bmsPath, fname, fdirectory });
                var arguments = string.Join(" ", args.Select(x => $"\"{x.Replace("\"", "'")}\""));
                using (var exeProcess = Process.Start(new ProcessStartInfo
                {
                    //CreateNoWindow = false,
                    //UseShellExecute = false,
                    //WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = fileName,
                    Arguments = arguments
                }))
                {
                    exeProcess.WaitForExit();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}