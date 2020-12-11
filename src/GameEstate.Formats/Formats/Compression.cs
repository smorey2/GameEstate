﻿using Compression;
using Compression.Doboz;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using K4os.Compression.LZ4;
using System;
using System.IO;
using System.Linq;

namespace GameEstate.Formats
{
    public static class Compression
    {
        const int BufferSize = 4096 * 10;

        public static byte[] DecompressZlib_old(this BinaryReader r, int length, int newLength)
        {
            var fileData = r.ReadBytes(length);
            var newFileData = new byte[newLength];
            using (var s = new MemoryStream(fileData))
            using (var gs = new InflaterInputStream(s))
                gs.Read(newFileData, 0, newFileData.Length);
            return newFileData;
        }
        public static byte[] DecompressZlib(this BinaryReader r, int length, int newLength)
        {
            var fileData = r.ReadBytes(length);
            var inflater = new Inflater();
            inflater.SetInput(fileData, 0, fileData.Length);
            int count;
            var buffer = new byte[BufferSize];
            using (var s = new MemoryStream())
            {
                while ((count = inflater.Inflate(buffer)) > 0)
                    s.Write(buffer, 0, count);
                return s.ToArray();
            }
        }

        public static byte[] DecompressSnappy(this BinaryReader r, int length, int newLength) => throw new NotSupportedException();

        public static byte[] DecompressDoboz(this BinaryReader r, int length, int newLength)
        {
            var fileData = r.ReadBytes(length);
            return DobozDecoder.Decode(fileData, 0, fileData.Length);
        }

        public static byte[] DecompressLz4(this BinaryReader r, int length, int newLength)
        {
            var fileData = r.ReadBytes(length);
            var newFileData = new byte[newLength];
            LZ4Codec.Decode(fileData, newFileData);
            return newFileData;
        }

        public static int DecompressLzfChunk(this BinaryReader r, int length, byte[] buffer)
        {
            var fileData = r.ReadBytes(length);
            return Lzf.Decompress(fileData, buffer);
        }

        public static byte[] DecompressOodleLZ(this BinaryReader r, int length, int newLength)
        {
            var oodleCompression = r.ReadBytes(4);
            if (!(oodleCompression.SequenceEqual(new byte[] { 0x4b, 0x41, 0x52, 0x4b })))
                throw new NotImplementedException();
            var size = r.ReadUInt32();
            if (size != newLength)
                throw new FileFormatException();
            var fileData = r.ReadBytes(length - 8);
            var newFileData = new byte[newLength];
            var unpackedSize = OodleLZ.Decompress(fileData, newFileData);
            if (unpackedSize != newLength)
                throw new FileFormatException($"Unpacked size does not match real size. {unpackedSize} vs {newLength}");
            return newFileData;
        }

        //public static byte[] DecompressLzm(this BinaryReader r, int length, int newLength)
        //{
        //    var data = r.ReadBytes(length);
        //    var inflater = new Inflater();
        //    inflater.SetInput(data, 0, data.Length);
        //    int count;
        //    var buffer = new byte[BufferSize];
        //    using (var s = new MemoryStream())
        //    {
        //        while ((count = Inflater.Inflate(buffer)) > 0)
        //            s.Write(buffer, 0, count);
        //        s.Position = 0;
        //        return s.ToArray();
        //    }
        //}

        //public static ulong DecompressLz4(this Stream input, uint length, Stream output)
        //{
        //    var written = 0UL;
        //    var data = new byte[length];
        //    input.Read(data, 0, data.Length);
        //    using (var ms = new MemoryStream(data, false))
        //    using (var lz4Stream = LZ4Decoder.Create(ms, LZ4StreamMode.Read))
        //    {
        //        var buffer = new byte[BufferSize];
        //        int count;
        //        while ((count = lz4Stream.Read(buffer, 0, buffer.Length)) > 0)
        //        {
        //            output.Write(buffer, 0, count);
        //            written += (ulong)count;
        //        }
        //    }
        //}
    }
}