using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameEstate.Core
{
    public abstract class CorePakFile : IDisposable
    {
        public class FileMetadata
        {
            public long Position;
            public bool Compressed;
            public string Path;
            public int Size;
        }

        public readonly string FilePath;
        internal List<FileMetadata> _files = new List<FileMetadata>();
        BinaryReaderPool _pool;

        public CorePakFile(string filePath)
        {
            if (filePath == null)
                return;
            FilePath = filePath;
            _pool = new BinaryReaderPool(filePath);
            var r = _pool.Get();
            try { ReadMetadata(r); }
            finally { _pool.Release(r); }
        }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~CorePakFile() => Close();

        public void Close()
        {
            _pool?.Dispose();
            _pool = null;
        }

        protected abstract void ReadMetadata(BinaryReader r);

        protected void ReadOne(BinaryReader r, Func<byte[], bool> hasRecord)
        {
            r.BaseStream.Seek(0, SeekOrigin.Begin);
            var chunk = new byte[16];
            var buf = new byte[100];
            // read in 16 bytes chunks
            while (r.Read(chunk, 0, 16) != 0)
            {
                if (!hasRecord(chunk))
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
                var fileSize = BitConverter.ToInt32(buf, fileNameSize + 12);

                // skip extra
                var extraFieldRead = ((extraFieldSize + 15) & ~15) - (charIdx != 0 ? 32 : 16);
                r.Skip(extraFieldRead); //: var extraField = new byte[extraFieldRead]; _r.Read(extraField, 0, extraField.Length);

                // add
                _files.Add(new FileMetadata
                {
                    Position = r.BaseStream.Position,
                    Compressed = compressed,
                    Path = fileName,
                    Size = fileSize,
                });

                // file data
                r.Skip(fileSize + (16 - (fileSize % 16))); //: var file = new byte[fileSize]; _r.Read(file, 0, fileSize); _r.Position += 16 - (fileSize % 16);
            }
        }
    }
}