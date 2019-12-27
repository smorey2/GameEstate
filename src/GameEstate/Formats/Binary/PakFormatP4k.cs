using GameEstate.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class PakFormatP4k : PakFormat
    {
        readonly byte[] Magic;

        public PakFormatP4k(byte[] magic) => Magic = magic;

        public override Task ReadAsync(CorePakFile source, BinaryReader r)
        {
            var files = source.Files = new List<FileMetadata>();
            var chunk = new byte[16];
            var buf = new byte[100];
            // read in 16 bytes chunks
            while (r.Read(chunk, 0, 0x10) != 0)
            {
                if (chunk[0] != Magic[0] || chunk[1] != Magic[1] || chunk[2] != Magic[2] || chunk[3] != Magic[3])
                    continue;
                // file found
                var compressed = BitConverter.ToInt16(chunk, 8) == 0x64;
                r.Read(chunk, 0, 14); //: minus 2
                var fileNameSize = BitConverter.ToInt16(chunk, 0xA);
                var extraFieldSize = BitConverter.ToInt16(chunk, 0xC);

                // file name
                var fileNameRead = 2 + ((fileNameSize - 2 + 15) & ~15) + 16;
                if (fileNameRead > buf.Length) buf = new byte[fileNameRead];
                r.Read(buf, 0, fileNameRead);
                var fileName = Encoding.ASCII.GetString(buf, 0, fileNameSize);
                var charIdx = (fileNameSize - 2) % 16;

                // file size
                var packedSize = BitConverter.ToUInt32(buf, fileNameSize + 12);

                // skip extra
                var extraFieldRead = ((extraFieldSize + 15) & ~15) - (charIdx != 0 ? 32 : 16);
                r.Skip(extraFieldRead); //: var extraField = new byte[extraFieldRead]; r.Read(extraField, 0, extraField.Length);

                // add
                files.Add(new FileMetadata
                {
                    Position = r.BaseStream.Position,
                    Compressed = compressed,
                    Path = fileName,
                    PackedSize = packedSize,
                });

                // file data
                r.Skip(packedSize + (16 - (packedSize % 16))); //: var file = new byte[fileSize]; _r.Read(file, 0, fileSize); _r.Position += 16 - (fileSize % 16);
            }
            return Task.CompletedTask;
        }

        //public override Task WriteAsync(CorePakFile source, BinaryWriter w)
        //{
        //    var files = source.Files = new List<FileMetadata>();
        //    r.BaseStream.Seek(0, SeekOrigin.Begin);
        //    var chunk = new byte[16];
        //    var buf = new byte[100];
        //    // read in 16 bytes chunks
        //    while (r.Read(chunk, 0, 0x10) != 0)
        //    {
        //        if (chunk[0] != Magic[0] || chunk[1] != Magic[1] || chunk[2] != Magic[2] || chunk[3] != Magic[3])
        //            continue;
        //        // file found
        //        var compressed = BitConverter.ToInt16(chunk, 8) == 0x64;
        //        r.Read(chunk, 0, 14); //: minus 2
        //        var fileNameSize = BitConverter.ToInt16(chunk, 0xA);
        //        var extraFieldSize = BitConverter.ToInt16(chunk, 0xC);

        //        // file name
        //        var fileNameRead = 2 + ((fileNameSize - 2 + 15) & ~15) + 16;
        //        if (fileNameRead > buf.Length) buf = new byte[fileNameRead];
        //        r.Read(buf, 0, fileNameRead);
        //        var fileName = Encoding.ASCII.GetString(buf, 0, fileNameSize);
        //        var charIdx = (fileNameSize - 2) % 16;

        //        // file size
        //        var packedSize = BitConverter.ToUInt32(buf, fileNameSize + 12);

        //        // skip extra
        //        var extraFieldRead = ((extraFieldSize + 15) & ~15) - (charIdx != 0 ? 32 : 16);
        //        r.Skip(extraFieldRead); //: var extraField = new byte[extraFieldRead]; r.Read(extraField, 0, extraField.Length);

        //        // add
        //        files.Add(new FileMetadata
        //        {
        //            Position = r.BaseStream.Position,
        //            Compressed = compressed,
        //            Path = fileName,
        //            PackedSize = packedSize,
        //        });

        //        // file data
        //        r.Skip(packedSize + (16 - (packedSize % 16))); //: var file = new byte[fileSize]; _r.Read(file, 0, fileSize); _r.Position += 16 - (fileSize % 16);
        //    }
        //    return Task.CompletedTask;
        //}
    }
}