using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace GameEstate.Dev
{
    public static class Quickbms
    {
        static void WithTmpFile(string body, Action<string> action)
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

        static void Launch(string bmsPath, string fname, string fdirectory)
        {
            var fileName = Path.Combine("x86", "quickbms.exe");
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
            }
            catch
            {
            }
        }

        public static void Load(string bms, string fname, string fdirectory)
        {
            WithTmpFile(bms, bmsPath =>
            {
                Launch(bmsPath, fname, fdirectory);
            });
        }
    }
}