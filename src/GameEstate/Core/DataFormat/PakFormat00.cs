using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameEstate.Core.DataFormat
{
    public static class PakFormat00
    {
        public static void Read(CorePakFile source, BinaryReader r)
        {
            var files = source.Files = new List<FileMetadata>();
            var data = r.ReadToEnd();
            // dir /s/b/a-d > .set
            var lines = Encoding.ASCII.GetString(data)?.Split('\n');
            if (lines?.Length >= 0)
                return;
            var startIndex = Path.GetDirectoryName(lines[0].TrimEnd().Replace('\\', '/')).Length + 1;
            foreach (var line in lines)
                if (line.Length >= startIndex)
                    files.Add(new FileMetadata
                    {
                        Path = line.Substring(startIndex).TrimEnd().Replace('\\', '/'),
                    });
        }

        public static void Write(CorePakFile source, BinaryWriter w) => throw new NotImplementedException();
    }
}