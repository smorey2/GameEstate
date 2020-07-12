using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using static GameEstate.EstateDebug;

namespace GameEstate.Core
{
    public enum ASCIIFormat { PossiblyNullTerminated, ZeroPadded, ZeroTerminated }

    public static class CoreExtensions
    {
        #region System

        public static string GetEnumDescription(this Type source, string value)
        {
            var name = Enum.GetNames(source).FirstOrDefault(f => f.Equals(value, StringComparison.OrdinalIgnoreCase));
            if (name == null)
                return string.Empty;
            var field = source.GetField(name);
            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public static bool Equals(this string source, byte[] bytes)
        {
            if (bytes.Length != source.Length)
                return false;
            for (var i = 0; i < bytes.Length; i++)
                if (bytes[i] != source[i])
                    return false;
            return true;
        }

        #endregion

        #region Convert Color

        public static uint FromBGR555(this ushort bgr555, bool addAlpha = true)
        {
            var a = addAlpha ? (byte)0xFF : (byte)0;
            var r = (byte)Math.Min(((bgr555 & 0x7C00) >> 10) * 8, byte.MaxValue);
            var g = (byte)Math.Min(((bgr555 & 0x03E0) >> 5) * 8, byte.MaxValue);
            var b = (byte)Math.Min(((bgr555 & 0x001F) >> 0) * 8, byte.MaxValue);
            var color =
                ((uint)(a << 24) & 0xFF000000) |
                ((uint)(r << 16) & 0x00FF0000) |
                ((uint)(g << 8) & 0x0000FF00) |
                ((uint)(b << 0) & 0x000000FF);
            return color;
        }

        #endregion

        #region Numeric

        public static float Get(this Matrix4x4 source, int row, int column)
        {
            if (row == 0)
            {
                if (column == 0) return source.M11;
                else if (column == 1) return source.M12;
                else if (column == 2) return source.M13;
                else if (column == 3) return source.M14;
                else throw new ArgumentOutOfRangeException(nameof(row));
            }
            else if (row == 1)
            {
                if (column == 0) return source.M21;
                else if (column == 1) return source.M22;
                else if (column == 2) return source.M23;
                else if (column == 3) return source.M24;
                else throw new ArgumentOutOfRangeException(nameof(row));
            }
            else if (row == 2)
            {
                if (column == 0) return source.M31;
                else if (column == 1) return source.M32;
                else if (column == 2) return source.M33;
                else if (column == 3) return source.M34;
                else throw new ArgumentOutOfRangeException(nameof(row));
            }
            else if (row == 3)
            {
                if (column == 0) return source.M41;
                else if (column == 1) return source.M42;
                else if (column == 2) return source.M43;
                else if (column == 3) return source.M44;
                else throw new ArgumentOutOfRangeException(nameof(row));
            }
            else throw new ArgumentOutOfRangeException(nameof(column));
        }

        public static void Set(this Matrix4x4 source, int row, int column, float value)
        {
            if (row == 0)
            {
                if (column == 0) source.M11 = value;
                else if (column == 1) source.M12 = value;
                else if (column == 2) source.M13 = value;
                else if (column == 3) source.M14 = value;
                else throw new ArgumentOutOfRangeException(nameof(row));
            }
            else if (row == 1)
            {
                if (column == 0) source.M21 = value;
                else if (column == 1) source.M22 = value;
                else if (column == 2) source.M23 = value;
                else if (column == 3) source.M24 = value;
                else throw new ArgumentOutOfRangeException(nameof(row));
            }
            else if (row == 2)
            {
                if (column == 0) source.M31 = value;
                else if (column == 1) source.M32 = value;
                else if (column == 2) source.M33 = value;
                else if (column == 3) source.M34 = value;
                else throw new ArgumentOutOfRangeException(nameof(row));
            }
            else if (row == 3)
            {
                if (column == 0) source.M41 = value;
                else if (column == 1) source.M42 = value;
                else if (column == 2) source.M43 = value;
                else if (column == 3) source.M44 = value;
                else throw new ArgumentOutOfRangeException(nameof(row));
            }
            else throw new ArgumentOutOfRangeException(nameof(column));
        }

        #endregion

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
        public static void Skip(this BinaryReader source, long count) => source.BaseStream.Seek(count, SeekOrigin.Current); // source.BaseStream.Position += count;

        public static byte[] ReadAbsoluteBytes(this BinaryReader source, long position, int count)
        {
            var last = source.BaseStream.Position;
            source.BaseStream.Position = position;
            var r = source.ReadBytes(count);
            source.BaseStream.Position = last;
            return r;
        }
        public static byte[] ReadToEnd(this BinaryReader source)
        {
            var length = (int)(source.BaseStream.Length - source.BaseStream.Position);
            Assert(length <= int.MaxValue);
            return source.ReadBytes(length);
        }
        public static void ReadToEnd(this BinaryReader source, byte[] buffer, int startIndex = 0)
        {
            var length = (int)source.BaseStream.Length - source.BaseStream.Position;
            Assert(startIndex >= 0 && length <= int.MaxValue && startIndex + length <= buffer.Length);
            source.Read(buffer, startIndex, (int)length);
        }


        public static byte[] ReadL32Bytes(this BinaryReader source) => source.ReadBytes((int)source.ReadUInt32());
        public static string ReadL32ASCII(this BinaryReader source) => Encoding.ASCII.GetString(source.ReadBytes((int)source.ReadUInt32()));
        public static string ReadL16ASCII(this BinaryReader source) => Encoding.ASCII.GetString(source.ReadBytes((int)source.ReadUInt16()));
        public static string ReadASCII(this BinaryReader source, int length) => Encoding.ASCII.GetString(source.ReadBytes(length));
        public static string ReadASCII(this BinaryReader source, int length, ASCIIFormat format)
        {
            var buf = source.ReadBytes(length);
            int i;
            switch (format)
            {
                case ASCIIFormat.PossiblyNullTerminated:
                    i = buf.Last() != 0 ? buf.Length : buf.Length - 1;
                    return Encoding.ASCII.GetString(buf, 0, i);
                case ASCIIFormat.ZeroPadded:
                    for (i = buf.Length - 1; i >= 0 && buf[i] == 0; i--) { }
                    return Encoding.ASCII.GetString(buf, 0, i + 1);
                case ASCIIFormat.ZeroTerminated:
                    for (i = 0; i < buf.Length && buf[i] != 0; i++) { }
                    return Encoding.ASCII.GetString(buf, 0, i);
                default: throw new ArgumentOutOfRangeException(nameof(format), format.ToString());
            }
        }

        public static string ReadZEncoding(this BinaryReader source, Encoding encoding)
        {
            var characterSize = encoding.GetByteCount("e");
            using (var s = new MemoryStream())
            {
                while (true)
                {
                    var data = new byte[characterSize];
                    source.Read(data, 0, characterSize);
                    if (encoding.GetString(data, 0, characterSize) == "\0")
                        break;
                    s.Write(data, 0, data.Length);
                }
                return encoding.GetString(s.ToArray());
            }
        }
        public static string ReadZUTF8(this BinaryReader source, int length = int.MaxValue, List<byte> buf = null)
        {
            if (buf == null)
                buf = new List<byte>(64);
            buf.Clear();
            byte c;
            while (length-- > 0 && (c = source.ReadByte()) != 0)
                buf.Add(c);
            return Encoding.UTF8.GetString(buf.ToArray());
        }
        public static string ReadZASCII(this BinaryReader source, int length = int.MaxValue, List<byte> buf = null)
        {
            if (buf == null)
                buf = new List<byte>(64);
            buf.Clear();
            byte c;
            while (length-- > 0 && (c = source.ReadByte()) != 0)
                buf.Add(c);
            return Encoding.ASCII.GetString(buf.ToArray());
        }
        public static string[] ReadZASCIIArray(this BinaryReader source, int length = int.MaxValue, List<byte> buf = null)
        {
            if (buf == null)
                buf = new List<byte>(64);
            var list = new List<string>();
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

        public static T ReadT<T>(this BinaryReader source, int length) => UnsafeUtils.MarshalT<T>(source.ReadBytes(length));
        public static T[] ReadTArray<T>(this BinaryReader source, int sizeOf, int count) => UnsafeUtils.MarshalTArray<T>(source.ReadBytes(count * sizeOf), 0, count);

        //public static T[] ReadTArray2<T>(this BinaryReader source, int sizeOf, int count) where T : struct { var r = new T[count]; Buffer.BlockCopy(source.ReadBytes(count * sizeOf), 0, r, 0, count * sizeOf); return r; }
        public static T[] ReadTArray2<T>(this BinaryReader source, int sizeOf, int count) => UnsafeUtils.MarshalTArray<T>(source.ReadBytes(count * sizeOf), 0, count);
        public static T[] ReadTMany<T>(this BinaryReader source, int length, int count) => UnsafeUtils.MarshalTArray<T>(source.ReadBytes(length), 0, count);
        public static void ReadTMany<T>(this BinaryReader source, T[] dest, int length) => UnsafeUtils.MarshalTArray<T>((FileStream)source.BaseStream, dest, 0, length);

        public static bool ReadBool32(this BinaryReader source) => source.ReadUInt32() != 0;

        public static Vector2 ReadVector2(this BinaryReader source) => new Vector2(source.ReadSingle(), source.ReadSingle());
        public static Vector3 ReadVector3(this BinaryReader source) => new Vector3(source.ReadSingle(), source.ReadSingle(), source.ReadSingle());
        public static Vector4 ReadVector4(this BinaryReader source) => new Vector4(source.ReadSingle(), source.ReadSingle(), source.ReadSingle(), source.ReadSingle());
        /// <summary>
        /// Reads a column-major 3x3 matrix but returns a functionally equivalent 4x4 matrix.
        /// </summary>
        public static Matrix4x4 ReadColumnMajorMatrix3x3(this BinaryReader source)
        {
            var matrix = new Matrix4x4();
            for (var columnIndex = 0; columnIndex < 4; columnIndex++)
                for (var rowIndex = 0; rowIndex < 4; rowIndex++)
                {
                    // If we're in the 3x3 part of the matrix, read values. Otherwise, use the identity matrix.
                    if (rowIndex <= 2 && columnIndex <= 2) matrix.Set(rowIndex, columnIndex, source.ReadSingle());
                    else matrix.Set(rowIndex, columnIndex, rowIndex == columnIndex ? 1f : 0f);
                }
            return matrix;
        }
        /// <summary>
        /// Reads a row-major 3x3 matrix but returns a functionally equivalent 4x4 matrix.
        /// </summary>
        public static Matrix4x4 ReadRowMajorMatrix3x3(this BinaryReader source)
        {
            var matrix = new Matrix4x4();
            for (var rowIndex = 0; rowIndex < 4; rowIndex++)
                for (var columnIndex = 0; columnIndex < 4; columnIndex++)
                {
                    // If we're in the 3x3 part of the matrix, read values. Otherwise, use the identity matrix.
                    if (rowIndex <= 2 && columnIndex <= 2) matrix.Set(rowIndex, columnIndex, source.ReadSingle());
                    else matrix.Set(rowIndex, columnIndex, rowIndex == columnIndex ? 1f : 0f);
                }
            return matrix;
        }
        public static Matrix4x4 ReadColumnMajorMatrix4x4(this BinaryReader source)
        {
            var matrix = new Matrix4x4();
            for (var columnIndex = 0; columnIndex < 4; columnIndex++)
                for (var rowIndex = 0; rowIndex < 4; rowIndex++)
                    matrix.Set(rowIndex, columnIndex, source.ReadSingle());
            return matrix;
        }
        public static Matrix4x4 ReadRowMajorMatrix4x4(this BinaryReader source)
        {
            var matrix = new Matrix4x4();
            for (var rowIndex = 0; rowIndex < 4; rowIndex++)
                for (var columnIndex = 0; columnIndex < 4; columnIndex++)
                    matrix.Set(rowIndex, columnIndex, source.ReadSingle());
            return matrix;
        }
        public static Quaternion ReadQuaternionWFirst(this BinaryReader source)
        {
            var w = source.ReadSingle();
            var x = source.ReadSingle();
            var y = source.ReadSingle();
            var z = source.ReadSingle();
            return new Quaternion(x, y, z, w);
        }
        public static Quaternion ReadLEQuaternionWLast(this BinaryReader source)
        {
            var x = source.ReadSingle();
            var y = source.ReadSingle();
            var z = source.ReadSingle();
            var w = source.ReadSingle();
            return new Quaternion(x, y, z, w);
        }

        //public static string ReadZString() => ReadByte() != 0 ? ReadString() : null;
        //public static DateTime ReadDeltaTime(this BinaryReader source)
        //{
        //    var ticks = ReadInt64();
        //    var now = DateTime.UtcNow.Ticks;
        //    if (ticks > 0 && (ticks + now) < 0) return DateTime.MaxValue;
        //    else if (ticks < 0 && (ticks + now) < 0) return DateTime.MinValue;
        //    try { return new DateTime(now + ticks); }
        //    catch { return ticks > 0 ? DateTime.MaxValue : DateTime.MinValue; }
        //}
        //public static IPAddress ReadIPAddress(this BinaryReader source) => new IPAddress(ReadInt64());
        //public static int ReadEncodedInt(this BinaryReader source)
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
        //public static DateTime ReadDateTime(this BinaryReader source) => new DateTime(ReadInt64());
        //public static DateTimeOffset ReadDateTimeOffset(this BinaryReader source)
        //{
        //    var ticks = ReadInt64();
        //    var offset = new TimeSpan(ReadInt64());
        //    return new DateTimeOffset(ticks, offset);
        //}
        //public static TimeSpan ReadTimeSpan(this BinaryReader source) => new TimeSpan(ReadInt64());

        //public static Point3D ReadPoint3D(this BinaryReader source) => new Point3D(ReadInt32(), ReadInt32(), ReadInt32());
        //public static Point2D ReadPoint2D(this BinaryReader source) => new Point2D(ReadInt32(), ReadInt32());
        //public static Rectangle2D ReadRect2D(this BinaryReader source) => new Rectangle2D(ReadPoint2D(), ReadPoint2D());
        //public static Rectangle3D ReadRect3D(this BinaryReader source) => new Rectangle3D(ReadPoint3D(), ReadPoint3D());

        #endregion

        #region Sequence

        public static T Last<T>(this T[] source) { Assert(source.Length > 0); return source[source.Length - 1]; }
        public static T Last<T>(this List<T> source) { Assert(source.Count > 0); return source[source.Count - 1]; }

        /// <summary>
        /// Calculates the minimum and maximum values of an array.
        /// </summary>
        public static void GetExtrema(this float[] source, out float min, out float max)
        {
            min = float.MaxValue; max = float.MinValue;
            foreach (var element in source) { min = Math.Min(min, element); max = Math.Max(max, element); }
        }
        /// <summary>
        /// Calculates the minimum and maximum values of a 2D array.
        /// </summary>
        public static void GetExtrema(this float[,] source, out float min, out float max)
        {
            min = float.MaxValue; max = float.MinValue;
            foreach (var element in source) { min = Math.Min(min, element); max = Math.Max(max, element); }
        }
        /// <summary>
        /// Calculates the minimum and maximum values of a 3D array.
        /// </summary>
        public static void GetExtrema(this float[,,] source, out float min, out float max)
        {
            min = float.MaxValue; max = float.MinValue;
            foreach (var element in source) { min = Math.Min(min, element); max = Math.Max(max, element); }
        }

        public static void Flip2DArrayVertically<T>(this T[] source, int rowCount, int columnCount) => Flip2DSubArrayVertically(source, 0, rowCount, columnCount);
        /// <summary>
        /// Flips a portion of a 2D array vertically.
        /// </summary>
        /// <param name="source">A 2D array represented as a 1D row-major array.</param>
        /// <param name="startIndex">The 1D index of the top left element in the portion of the 2D array we want to flip.</param>
        /// <param name="rows">The number of rows in the sub-array.</param>
        /// <param name="bytesPerRow">The number of columns in the sub-array.</param>
        public static void Flip2DSubArrayVertically<T>(this T[] source, int startIndex, int rows, int bytesPerRow)
        {
            Assert(startIndex >= 0 && rows >= 0 && bytesPerRow >= 0 && (startIndex + (rows * bytesPerRow)) <= source.Length);
            var tmpRow = new T[bytesPerRow];
            var lastRowIndex = rows - 1;
            for (var rowIndex = 0; rowIndex < (rows / 2); rowIndex++)
            {
                var otherRowIndex = lastRowIndex - rowIndex;
                var rowStartIndex = startIndex + (rowIndex * bytesPerRow);
                var otherRowStartIndex = startIndex + (otherRowIndex * bytesPerRow);
                Array.Copy(source, otherRowStartIndex, tmpRow, 0, bytesPerRow); // other -> tmp
                Array.Copy(source, rowStartIndex, source, otherRowStartIndex, bytesPerRow); // row -> other
                Array.Copy(tmpRow, 0, source, rowStartIndex, bytesPerRow); // tmp -> row
            }
        }

        #endregion
    }
}