using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEstate.Core.DataFormat
{
    public class PakFormat00 : PakFormat
    {
        public override Task ReadAsync(CorePakFile source, BinaryReader r)
        {
            var files = source.Files = new List<FileMetadata>();
            var data = r.ReadToEnd();
            // dir /s/b/a-d > .set
            var lines = Encoding.ASCII.GetString(data)?.Split('\n');
            if (lines?.Length == 0)
                return Task.CompletedTask;
            string path;
            var startIndex = Path.GetDirectoryName(lines[0].TrimEnd().Replace('\\', '/')).Length + 1;
            foreach (var line in lines)
                if (line.Length >= startIndex && (path = line.Substring(startIndex).TrimEnd().Replace('\\', '/')) != ".set")
                {
                    files.Add(new FileMetadata
                    {
                        Path = path,
                    });
                }
            return Task.CompletedTask;
        }

        public override Task WriteAsync(CorePakFile source, BinaryWriter w, WriteState stage)
        {
            var path = Encoding.ASCII.GetBytes($@"C:\{Path.GetFileNameWithoutExtension(source.FilePath)}\");
            w.Write(path);
            w.Write(Encoding.ASCII.GetBytes(".set"));
            w.Write('\n');
            w.Flush();
            // files
            var files = source.Files;
            foreach (var file in files.OrderBy(x => x.Path))
            {
                w.Write(path);
                w.Write(Encoding.ASCII.GetBytes(file.Path));
                w.Write('\n');
                w.Flush();
            }
            return Task.CompletedTask;
        }
    }
}