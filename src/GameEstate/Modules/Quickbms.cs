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

        public static async Task<int> ExtractPackageAsync(string packagePath, string scriptPath, string contentPath)
        {
            if (packagePath == null)
                throw new ArgumentNullException(nameof(packagePath));
            if (scriptPath == null)
                throw new ArgumentNullException(nameof(scriptPath));
            if (contentPath == null)
                throw new ArgumentNullException(nameof(contentPath));
            if (contentPath.Contains("**"))
                return await Task.Run(() => ProcessLaunch(scriptPath, contentPath.Replace("**", "*"), packagePath));
            var searchPattern = Path.GetFileName(contentPath);
            contentPath = Path.GetDirectoryName(contentPath);
            var paths = Directory.GetDirectories(contentPath, searchPattern).Union(Directory.GetFiles(contentPath, searchPattern));
            var returnCode = 0;
            Parallel.ForEach(paths, new ParallelOptions { MaxDegreeOfParallelism = -1 }, x => { returnCode |= ProcessLaunch(scriptPath, x, packagePath); });
            return returnCode;
        }

        static int ProcessLaunch(string bmsPath, string fname, string fdirectory, bool is4gb = false)
        {
            var fileName = Path.Combine("Modules", is4gb ? "quickbms_4gb_files.exe" : "quickbms.exe");
            try
            {
                var args = new List<string>(new[] { "-Y", "-z" });
                //if (false)
                //    args.AddRange(new[] { "-f", "" });
                args.AddRange(new[] { bmsPath, fname, fdirectory });
                var arguments = string.Join(" ", args.Select(x => $"\"{x.Replace("\"", "'")}\""));
                using (var process = Process.Start(new ProcessStartInfo
                {
                    CreateNoWindow = false,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = fileName,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                }))
                {
                    //process.OutputDataReceived += (s, e) => Console.WriteLine(e.Data);
                    //process.ErrorDataReceived += (s, e) => Console.WriteLine(e.Data);
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();
                    return process.ExitCode;
                }
            }
            catch { return -1; }
        }
    }
}