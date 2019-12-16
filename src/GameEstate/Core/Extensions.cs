using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameEstate.Core
{
    public enum ASCIIFormat { Raw, PossiblyNullTerminated, ZeroPadded, ZeroTerminated }

    public static class Extensions
    {
        #region Random

        public static class Random
        {
            static System.Random _random = new System.Random();
            public static int RandomValue(int low, int high) => _random.Next(low, high + 1);
        }

        #endregion

        #region BinaryReader

        public static long Position(this BinaryReader source) => source.BaseStream.Position;
        public static void Position(this BinaryReader source, long position) => source.BaseStream.Position = position;
        public static void Seek(this BinaryReader source, long offset, SeekOrigin origin) => source.BaseStream.Seek(offset, origin);
        public static void Skip(this BinaryReader source, long count) => source.BaseStream.Position += count;

        public static byte[] ReadAbsoluteBytes(this BinaryReader source, long position, int count)
        {
            var last = source.BaseStream.Position;
            source.BaseStream.Position = position;
            var r = source.ReadBytes(count);
            source.BaseStream.Position = last;
            return r;
        }
        //public override byte[] ReadRestOfBytes()
        //{
        //    var remainingByteCount = BaseStream.Length - BaseStream.Position;
        //    Assert(remainingByteCount <= int.MaxValue);
        //    return ReadBytes((int)remainingByteCount);
        //}
        //public override void ReadRestOfBytes(byte[] buffer, int startIndex)
        //{
        //    var remainingByteCount = BaseStream.Length - BaseStream.Position;
        //    Assert(startIndex >= 0 && remainingByteCount <= int.MaxValue && startIndex + remainingByteCount <= buffer.Length);
        //    Read(buffer, startIndex, (int)remainingByteCount);
        //}
        public static string ReadASCII(this BinaryReader source, int length, ASCIIFormat format = ASCIIFormat.Raw)
        {
            Debug.Assert(length >= 0);
            var buf = source.ReadBytes(length);
            int i;
            switch (format)
            {
                case ASCIIFormat.Raw:
                    return Encoding.ASCII.GetString(buf);
                case ASCIIFormat.PossiblyNullTerminated:
                    var bytesIdx = buf.Last() != 0 ? buf.Length : buf.Length - 1;
                    return Encoding.ASCII.GetString(buf, 0, bytesIdx);
                case ASCIIFormat.ZeroPadded:
                    for (i = buf.Length - 1; i >= 0 && buf[i] == 0; i--) { }
                    return Encoding.ASCII.GetString(buf, 0, i + 1);
                case ASCIIFormat.ZeroTerminated:
                    for (i = 0; i <= buf.Length && buf[i] != 0; i++) { }
                    return Encoding.ASCII.GetString(buf, 0, i);
                default: throw new ArgumentOutOfRangeException(nameof(format), format.ToString());
            }
        }
        public static string[] ReadASCIIArray(this BinaryReader source, int length, int bufSize = 64)
        {
            var list = new List<string>();
            var buf = new List<byte>(bufSize);
            while (length > 0)
            {
                buf.Clear();
                byte c;
                while (length-- > 0 && (c = source.ReadByte()) != 0)
                    buf.Add(c);
                list.Add(Encoding.ASCII.GetString(buf.ToArray()));
            }
            return list.ToArray();
        }

        public static T ReadT<T>(this BinaryReader source, int length) => UnsafeUtils.MarshalT<T>(source.ReadBytes(length), length);
        public static T[] ReadTArray<T>(this BinaryReader source, int sizeOf, int count) => UnsafeUtils.MarshalTArray<T>(source.ReadBytes(count * sizeOf), count);
        public static T[] ReadTMany<T>(this BinaryReader source, int sizeOf, int count) where T : struct { var r = new T[count]; Buffer.BlockCopy(source.ReadBytes(count * sizeOf), 0, r, 0, count * sizeOf); return r; }


        //public override bool ReadBool32() => ReadUInt32() != 0;
        //public override byte[] ReadLength32PrefixedBytes() => ReadBytes((int)ReadUInt32());
        //public override string ReadLength32PrefixedASCIIString() => Encoding.ASCII.GetString(ReadLength32PrefixedBytes());
        //public override Vector2 ReadVector2() => new Vector2(ReadSingle(), ReadSingle());
        //public override Vector3 ReadVector3() => new Vector3(ReadSingle(), ReadSingle(), ReadSingle());
        //public override Vector4 ReadVector4() => new Vector4(ReadSingle(), ReadSingle(), ReadSingle(), ReadSingle());
        ///// <summary>
        ///// Reads a column-major 3x3 matrix but returns a functionally equivalent 4x4 matrix.
        ///// </summary>
        //public override Matrix4x4 ReadColumnMajorMatrix3x3()
        //{
        //    var matrix = new Matrix4x4();
        //    for (var columnIndex = 0; columnIndex < 4; columnIndex++)
        //        for (var rowIndex = 0; rowIndex < 4; rowIndex++)
        //        {
        //            // If we're in the 3x3 part of the matrix, read values. Otherwise, use the identity matrix.
        //            if (rowIndex <= 2 && columnIndex <= 2) matrix[rowIndex, columnIndex] = ReadSingle();
        //            else matrix[rowIndex, columnIndex] = rowIndex == columnIndex ? 1 : 0;
        //        }
        //    return matrix;
        //}
        ///// <summary>
        ///// Reads a row-major 3x3 matrix but returns a functionally equivalent 4x4 matrix.
        ///// </summary>
        //public override Matrix4x4 ReadRowMajorMatrix3x3()
        //{
        //    var matrix = new Matrix4x4();
        //    for (var rowIndex = 0; rowIndex < 4; rowIndex++)
        //        for (var columnIndex = 0; columnIndex < 4; columnIndex++)
        //        {
        //            // If we're in the 3x3 part of the matrix, read values. Otherwise, use the identity matrix.
        //            if (rowIndex <= 2 && columnIndex <= 2) matrix[rowIndex, columnIndex] = ReadSingle();
        //            else matrix[rowIndex, columnIndex] = rowIndex == columnIndex ? 1 : 0;
        //        }
        //    return matrix;
        //}
        //public override Matrix4x4 ReadColumnMajorMatrix4x4()
        //{
        //    var matrix = new Matrix4x4();
        //    for (var columnIndex = 0; columnIndex < 4; columnIndex++)
        //        for (var rowIndex = 0; rowIndex < 4; rowIndex++)
        //            matrix[rowIndex, columnIndex] = ReadSingle();
        //    return matrix;
        //}
        //public override Matrix4x4 ReadRowMajorMatrix4x4()
        //{
        //    var matrix = new Matrix4x4();
        //    for (var rowIndex = 0; rowIndex < 4; rowIndex++)
        //        for (var columnIndex = 0; columnIndex < 4; columnIndex++)
        //            matrix[rowIndex, columnIndex] = ReadSingle();
        //    return matrix;
        //}
        //public override Quaternion ReadQuaternionWFirst()
        //{
        //    var w = ReadSingle();
        //    var x = ReadSingle();
        //    var y = ReadSingle();
        //    var z = ReadSingle();
        //    return new Quaternion(x, y, z, w);
        //}
        //public override Quaternion ReadLEQuaternionWLast()
        //{
        //    var x = ReadSingle();
        //    var y = ReadSingle();
        //    var z = ReadSingle();
        //    var w = ReadSingle();
        //    return new Quaternion(x, y, z, w);
        //}

        //public override string ReadZString() => ReadByte() != 0 ? ReadString() : null;
        //public override DateTime ReadDeltaTime()
        //{
        //    var ticks = ReadInt64();
        //    var now = DateTime.UtcNow.Ticks;
        //    if (ticks > 0 && (ticks + now) < 0) return DateTime.MaxValue;
        //    else if (ticks < 0 && (ticks + now) < 0) return DateTime.MinValue;
        //    try { return new DateTime(now + ticks); }
        //    catch { return ticks > 0 ? DateTime.MaxValue : DateTime.MinValue; }
        //}
        //public override IPAddress ReadIPAddress() => new IPAddress(ReadInt64());
        //public override int ReadEncodedInt()
        //{
        //    int v = 0, shift = 0;
        //    byte b;
        //    do
        //    {
        //        b = ReadByte();
        //        v |= (b & 0x7F) << shift;
        //        shift += 7;
        //    } while (b >= 0x80);
        //    return v;
        //}
        //public override DateTime ReadDateTime() => new DateTime(ReadInt64());
        //public override DateTimeOffset ReadDateTimeOffset()
        //{
        //    var ticks = ReadInt64();
        //    var offset = new TimeSpan(ReadInt64());
        //    return new DateTimeOffset(ticks, offset);
        //}
        //public override TimeSpan ReadTimeSpan() => new TimeSpan(ReadInt64());

        //public override Point3D ReadPoint3D() => new Point3D(ReadInt32(), ReadInt32(), ReadInt32());
        //public override Point2D ReadPoint2D() => new Point2D(ReadInt32(), ReadInt32());
        //public override Rectangle2D ReadRect2D() => new Rectangle2D(ReadPoint2D(), ReadPoint2D());
        //public override Rectangle3D ReadRect3D() => new Rectangle3D(ReadPoint3D(), ReadPoint3D());



        #endregion
    }
}