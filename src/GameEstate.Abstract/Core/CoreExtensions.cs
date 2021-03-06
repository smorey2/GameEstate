﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using static GameEstate.EstateDebug;

namespace GameEstate.Core
{
    public enum ASCIIFormat { PossiblyNullTerminated, ZeroPadded, ZeroTerminated }

    public static class CoreExtensions
    {
        public static readonly float EPSILON = 0.00019999999f;

        #region System

        public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? (T)attributes[0] : null;
        }

        /// <summary>
        /// Returns a list of flags for enum
        /// </summary>
        public static List<Enum> GetFlags(this Enum e) => Enum.GetValues(e.GetType()).Cast<Enum>().Where(e.HasFlag).ToList();

        /// <summary>
        /// Returns the # of bits set in a Flags enum
        /// </summary>
        /// <param name="enumVal">The enum uint value</param>
        public static int EnumNumFlags(uint enumVal)
        {
            var cnt = 0;
            while (enumVal != 0)
            {
                enumVal &= enumVal - 1; // remove the next set bit
                cnt++;
            }
            return cnt;
        }

        /// <summary>
        /// Returns TRUE if this flags enum has multiple flags set
        /// </summary>
        /// <param name="enumVal">The enum uint value</param>
        public static bool EnumHasMultiple(uint enumVal) => (enumVal & (enumVal - 1)) != 0;

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

        static readonly MethodInfo Enumerable_CastMethod = typeof(Enumerable).GetMethod("Cast");
        static readonly MethodInfo Enumerable_ToArrayMethod = typeof(Enumerable).GetMethod("ToArray");

        /// <summary>
        /// Casts to array.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object CastToArray(this IEnumerable source, Type type)
            => Enumerable_ToArrayMethod.MakeGenericMethod(type).Invoke(null, new[] { Enumerable_CastMethod.MakeGenericMethod(type).Invoke(null, new[] { source }) });

        public static string Reverse(this string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
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

        public static bool IsZero(this Vector3 v) => v.X == 0 && v.Y == 0 && v.Z == 0;

        public static bool IsZeroEpsilon(this Vector3 v) => Math.Abs(v.X) <= EPSILON && Math.Abs(v.Y) <= EPSILON && Math.Abs(v.Z) <= EPSILON;

        public static bool NearZero(this Vector3 v) => Math.Abs(v.X) <= 1.0f && Math.Abs(v.Y) <= 1.0f && Math.Abs(v.Z) <= 1.0f;

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

        #region Stream

        public static byte[] ReadBytes(this Stream stream, int count)
        {
            var data = new byte[count];
            stream.Read(data, 0, count);
            return data;
        }

        public static void WriteBytes(this Stream stream, byte[] data) => stream.Write(data, 0, data.Length);
        public static void WriteBytes(this Stream stream, BinaryReader r, int count)
        {
            var data = r.ReadBytes(count);
            stream.Write(data, 0, data.Length);
        }

        #endregion

        #region BinaryWriter

        public static long Position(this BinaryWriter source) => source.BaseStream.Position;

        public static void WriteBytes(this BinaryWriter source, byte[] data) => source.Write(data, 0, data.Length);
        public static void WriteT<T>(this BinaryWriter source, T value, int length) => source.WriteBytes(UnsafeUtils.MarshalF(value, length));
        //public static void WriteTArray<T>(this BinaryWriter source, int sizeOf, int count) => UnsafeUtils.MarshalFArray<T>(source.ReadBytes(count * sizeOf), 0, count);

        #endregion

        #region BinaryReader

        /// <summary>
        /// A Compressed UInt32 can be 1, 2, or 4 bytes.<para />
        /// If the first MSB (0x80) is 0, it is one byte.<para />
        /// If the first MSB (0x80) is set and the second MSB (0x40) is 0, it's 2 bytes.<para />
        /// If both (0x80) and (0x40) are set, it's 4 bytes.
        /// </summary>
        public static uint ReadCompressedUInt32(this BinaryReader source)
        {
            var b0 = source.ReadByte();
            if ((b0 & 0x80) == 0)
                return b0;
            var b1 = source.ReadByte();
            if ((b0 & 0x40) == 0)
                return (uint)(((b0 & 0x7F) << 8) | b1);
            var s = source.ReadUInt16();
            return (uint)(((((b0 & 0x3F) << 8) | b1) << 16) | s);
        }

        /// <summary>
        /// Aligns the stream to the next DWORD boundary.
        /// </summary>
        public static void AlignBoundary(this BinaryReader source)
        {
            var alignDelta = source.BaseStream.Position % 4;
            if (alignDelta != 0)
                source.BaseStream.Position += (int)(4 - alignDelta);
        }

        public static long Position(this BinaryReader source) => source.BaseStream.Position;
        public static void Position(this BinaryReader source, long position) => source.BaseStream.Position = position;
        public static long Position(this BinaryReader source, long position, int align)
        {
            if (position % 4 != 0)
                position += 4 - (position % 4);
            source.BaseStream.Position = position;
            return position;
        }
        public static void Seek(this BinaryReader source, long offset, SeekOrigin origin) => source.BaseStream.Seek(offset, origin);
        public static void Skip(this BinaryReader source, long count) => source.BaseStream.Position += count; //source.BaseStream.Seek(count, SeekOrigin.Current);

        public static void Peek(this BinaryReader source, Action<BinaryReader> action, int offset = 0)
        {
            var position = source.BaseStream.Position;
            if (offset != 0) source.BaseStream.Position += offset;
            action(source);
            source.BaseStream.Position = position;
        }
        public static T Peek<T>(this BinaryReader source, Func<BinaryReader, T> action, int offset = 0)
        {
            var position = source.BaseStream.Position;
            if (offset != 0) source.BaseStream.Position += offset;
            var value = action(source);
            source.BaseStream.Position = position;
            return value;
        }

        public static void CopyTo(this BinaryReader source, Stream destination, bool resetPosition = true)
        {
            source.BaseStream.CopyTo(destination);
            if (resetPosition) destination.Position = 0;
        }

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

        /// <summary>
        /// First reads a UInt16. If the MSB is set, it will be masked with 0x3FFF, shifted left 2 bytes, and then OR'd with the next UInt16. The sum is then added to knownType.
        /// </summary>
        public static uint ReadAsDataIDOfKnownType(this BinaryReader source, uint knownType)
        {
            var value = source.ReadUInt16();
            if ((value & 0x8000) != 0)
            {
                var lower = source.ReadUInt16();
                var higher = (value & 0x3FFF) << 16;
                return (uint)(knownType + (higher | lower));
            }
            return knownType + value;
        }

        public static Guid ReadGuid(this BinaryReader source) => new Guid(source.ReadBytes(16));

        public static string ReadString(this BinaryReader source, int length) => new string(source.ReadChars(length));
        public static string ReadZString(this BinaryReader source, char endChar = '\0', StringBuilder builder = null)
        {
            var b = builder ?? new StringBuilder();
            char c;
            while ((c = source.ReadChar()) != endChar)
                b.Append(c);
            var value = b.ToString();
            if (builder != null)
                builder.Length = 0;
            return value;
        }

        public static string ReadUnicodeString(this BinaryReader source) //: Needed?
        {
            var stringLength = source.ReadCompressedUInt32();
            var thestring = "";
            for (var i = 0; i < stringLength; i++)
            {
                var myChar = source.ReadUInt16();
                thestring += Convert.ToChar(myChar);
            }
            return thestring;
        }

        public static string ReadObfuscatedString(this BinaryReader source)
        {
            var stringlength = source.ReadUInt16();
            var thestring = source.ReadBytes(stringlength);
            for (var i = 0; i < stringlength; i++)
                // flip the bytes in the string to undo the obfuscation: i.e. 0xAB => 0xBA
                thestring[i] = (byte)((thestring[i] >> 4) | (thestring[i] << 4));
            return Encoding.GetEncoding(1252).GetString(thestring);
        }

        public static byte[] ReadL32Bytes(this BinaryReader source) => source.ReadBytes((int)source.ReadUInt32());

        public static string ReadL8ANSI(this BinaryReader source, Encoding encoding = null) => (encoding ?? Encoding.ASCII).GetString(source.ReadBytes(source.ReadByte()));
        public static string ReadL16ANSI(this BinaryReader source, Encoding encoding = null) => (encoding ?? Encoding.ASCII).GetString(source.ReadBytes(source.ReadUInt16()));
        public static string ReadL16ANSI(this BinaryReader source, bool nullTerminated, Encoding encoding = null) { var bytes = source.ReadBytes(source.ReadUInt16()); var newLength = bytes.Length - 1; return (encoding ?? Encoding.ASCII).GetString(bytes, 0, nullTerminated && bytes[newLength] == 0 ? newLength : bytes.Length); }
        public static string ReadL32ANSI(this BinaryReader source, Encoding encoding = null) => (encoding ?? Encoding.ASCII).GetString(source.ReadBytes((int)source.ReadUInt32()));
        public static string ReadL32ANSI(this BinaryReader source, bool nullTerminated, Encoding encoding = null) { var bytes = source.ReadBytes((int)source.ReadUInt32()); var newLength = bytes.Length - 1; return (encoding ?? Encoding.ASCII).GetString(bytes, 0, nullTerminated && bytes[newLength] == 0 ? newLength : bytes.Length); }
        public static string ReadC32ANSI(this BinaryReader source, Encoding encoding = null) => (encoding ?? Encoding.ASCII).GetString(source.ReadBytes((int)source.ReadCompressedUInt32()));
        public static string ReadC32ANSI(this BinaryReader source, bool nullTerminated, Encoding encoding = null) { var bytes = source.ReadBytes((int)source.ReadCompressedUInt32()); var newLength = bytes.Length - 1; return (encoding ?? Encoding.ASCII).GetString(bytes, 0, nullTerminated && bytes[newLength] == 0 ? newLength : bytes.Length); }
        public static string ReadANSI(this BinaryReader source, int length, Encoding encoding = null) => (encoding ?? Encoding.ASCII).GetString(source.ReadBytes(length));
        public static string ReadANSI(this BinaryReader source, int length, ASCIIFormat format, Encoding encoding = null)
        {
            var buf = source.ReadBytes(length);
            int i;
            switch (format)
            {
                case ASCIIFormat.PossiblyNullTerminated:
                    i = buf.Last() != 0 ? buf.Length : buf.Length - 1;
                    return (encoding ?? Encoding.ASCII).GetString(buf, 0, i);
                case ASCIIFormat.ZeroPadded:
                    for (i = buf.Length - 1; i >= 0 && buf[i] == 0; i--) { }
                    return (encoding ?? Encoding.ASCII).GetString(buf, 0, i + 1);
                case ASCIIFormat.ZeroTerminated:
                    for (i = 0; i < buf.Length && buf[i] != 0; i++) { }
                    return (encoding ?? Encoding.ASCII).GetString(buf, 0, i);
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

        public static string ReadO32Encoding(this BinaryReader source, Encoding encoding)
        {
            var currentOffset = source.BaseStream.Position;
            var offset = source.ReadUInt32();
            if (offset == 0)
                return string.Empty;
            source.BaseStream.Position = currentOffset + offset;
            var str = ReadZEncoding(source, encoding);
            source.BaseStream.Position = currentOffset + 4;
            return str;
        }

        public static string ReadO32UTF8(this BinaryReader source)
        {
            var currentOffset = source.BaseStream.Position;
            var offset = source.ReadUInt32();
            if (offset == 0)
                return string.Empty;
            source.BaseStream.Position = currentOffset + offset;
            var str = ReadZUTF8(source);
            source.BaseStream.Position = currentOffset + 4;
            return str;
        }

        public static string ReadZUTF8(this BinaryReader source, int length = int.MaxValue, MemoryStream buf = null)
        {
            if (buf == null)
                buf = new MemoryStream();
            buf.SetLength(0);
            byte c;
            while (length-- > 0 && (c = source.ReadByte()) != 0)
                buf.WriteByte(c);
            return Encoding.UTF8.GetString(buf.ToArray());
        }
        public static string ReadZASCII(this BinaryReader source, int length = int.MaxValue, MemoryStream buf = null)
        {
            if (buf == null)
                buf = new MemoryStream();
            buf.SetLength(0);
            byte c;
            while (length-- > 0 && (c = source.ReadByte()) != 0)
                buf.WriteByte(c);
            return Encoding.ASCII.GetString(buf.ToArray());
        }
        public static string[] ReadZASCIIArray(this BinaryReader source, int length = int.MaxValue, MemoryStream buf = null)
        {
            if (buf == null)
                buf = new MemoryStream();
            var list = new List<string>();
            while (length > 0)
            {
                buf.SetLength(0);
                byte c;
                while (length-- > 0 && (c = source.ReadByte()) != 0)
                    buf.WriteByte(c);
                list.Add(Encoding.ASCII.GetString(buf.ToArray()));
            }
            return list.ToArray();
        }

        public static T ReadT<T>(this BinaryReader source, int sizeOf) => UnsafeUtils.MarshalT<T>(source.ReadBytes(sizeOf));

        public static T[] ReadL16Array<T>(this BinaryReader source, int sizeOf) => ReadTArray<T>(source, sizeOf, source.ReadUInt16());
        public static T[] ReadL32Array<T>(this BinaryReader source, int sizeOf) => ReadTArray<T>(source, sizeOf, (int)source.ReadUInt32());
        public static T[] ReadC32Array<T>(this BinaryReader source, int sizeOf) => ReadTArray<T>(source, sizeOf, (int)source.ReadCompressedUInt32());
        public static T[] ReadTArray<T>(this BinaryReader source, int sizeOf, int count) => UnsafeUtils.MarshalTArray<T>(source.ReadBytes(count * sizeOf), 0, count);

        public static T[] ReadL16Array<T>(this BinaryReader source, Func<BinaryReader, T> factory) => ReadTArray(source, factory, source.ReadUInt16());
        public static T[] ReadL32Array<T>(this BinaryReader source, Func<BinaryReader, T> factory) => ReadTArray(source, factory, (int)source.ReadUInt32());
        public static T[] ReadC32Array<T>(this BinaryReader source, Func<BinaryReader, T> factory) => ReadTArray(source, factory, (int)source.ReadCompressedUInt32());
        public static T[] ReadTArray<T>(this BinaryReader source, Func<BinaryReader, T> factory, int count)
        {
            var list = new T[count];
            for (var i = 0; i < list.Length; i++)
                list[i] = factory(source);
            return list;
        }

        public static Dictionary<TKey, TValue> ReadL16Many<TKey, TValue>(this BinaryReader source, int keySizeOf, Func<BinaryReader, TValue> valueFactory, int offset = 0) => ReadTMany<TKey, TValue>(source, keySizeOf, valueFactory, source.ReadUInt16(), offset);
        public static Dictionary<TKey, TValue> ReadL32Many<TKey, TValue>(this BinaryReader source, int keySizeOf, Func<BinaryReader, TValue> valueFactory, int offset = 0) => ReadTMany<TKey, TValue>(source, keySizeOf, valueFactory, (int)source.ReadUInt32(), offset);
        public static Dictionary<TKey, TValue> ReadC32Many<TKey, TValue>(this BinaryReader source, int keySizeOf, Func<BinaryReader, TValue> valueFactory, int offset = 0) => ReadTMany<TKey, TValue>(source, keySizeOf, valueFactory, (int)source.ReadCompressedUInt32(), offset);
        public static Dictionary<TKey, TValue> ReadTMany<TKey, TValue>(this BinaryReader source, int keySizeOf, Func<BinaryReader, TValue> valueFactory, int count, int offset = 0)
        {
            if (offset != 0)
                source.Skip(offset);
            var set = new Dictionary<TKey, TValue>();
            for (var i = 0; i < count; i++)
                set.Add(source.ReadT<TKey>(keySizeOf), valueFactory(source));
            return set;
        }

        public static Dictionary<TKey, TValue> ReadL16Many<TKey, TValue>(this BinaryReader source, Func<BinaryReader, TKey> keyFactory, Func<BinaryReader, TValue> valueFactory, int offset = 0) => ReadTMany<TKey, TValue>(source, keyFactory, valueFactory, source.ReadUInt16(), offset);
        public static Dictionary<TKey, TValue> ReadL32Many<TKey, TValue>(this BinaryReader source, Func<BinaryReader, TKey> keyFactory, Func<BinaryReader, TValue> valueFactory, int offset = 0) => ReadTMany<TKey, TValue>(source, keyFactory, valueFactory, (int)source.ReadUInt32(), offset);
        public static Dictionary<TKey, TValue> ReadC32Many<TKey, TValue>(this BinaryReader source, Func<BinaryReader, TKey> keyFactory, Func<BinaryReader, TValue> valueFactory, int offset = 0) => ReadTMany<TKey, TValue>(source, keyFactory, valueFactory, (int)source.ReadCompressedUInt32(), offset);
        public static Dictionary<TKey, TValue> ReadTMany<TKey, TValue>(this BinaryReader source, Func<BinaryReader, TKey> keyFactory, Func<BinaryReader, TValue> valueFactory, int count, int offset = 0)
        {
            if (offset != 0)
                source.Skip(offset);
            var set = new Dictionary<TKey, TValue>();
            for (var i = 0; i < count; i++)
                set.Add(keyFactory(source), valueFactory(source));
            return set;
        }


        public static SortedDictionary<TKey, TValue> ReadL16SortedMany<TKey, TValue>(this BinaryReader source, int keySizeOf, Func<BinaryReader, TValue> valueFactory, int offset = 0) => ReadTSortedMany<TKey, TValue>(source, keySizeOf, valueFactory, source.ReadUInt16(), offset);
        public static SortedDictionary<TKey, TValue> ReadL32SortedMany<TKey, TValue>(this BinaryReader source, int keySizeOf, Func<BinaryReader, TValue> valueFactory, int offset = 0) => ReadTSortedMany<TKey, TValue>(source, keySizeOf, valueFactory, (int)source.ReadUInt32(), offset);
        public static SortedDictionary<TKey, TValue> ReadC32SortedMany<TKey, TValue>(this BinaryReader source, int keySizeOf, Func<BinaryReader, TValue> valueFactory, int offset = 0) => ReadTSortedMany<TKey, TValue>(source, keySizeOf, valueFactory, (int)source.ReadCompressedUInt32(), offset);
        public static SortedDictionary<TKey, TValue> ReadTSortedMany<TKey, TValue>(this BinaryReader source, int keySizeOf, Func<BinaryReader, TValue> valueFactory, int count, int offset = 0)
        {
            if (offset != 0)
                source.Skip(offset);
            var set = new SortedDictionary<TKey, TValue>();
            for (var i = 0; i < count; i++)
                set.Add(source.ReadT<TKey>(keySizeOf), valueFactory(source));
            return set;
        }


        //public static T[] ReadTArray2<T>(this BinaryReader source, int sizeOf, int count) where T : struct { var r = new T[count]; Buffer.BlockCopy(source.ReadBytes(count * sizeOf), 0, r, 0, count * sizeOf); return r; }
        //public static T[] ReadTArray2<T>(this BinaryReader source, int sizeOf, int count) => UnsafeUtils.MarshalTArray<T>(source.ReadBytes(count * sizeOf), 0, count);
        //public static T[] ReadTMany<T>(this BinaryReader source, int length, int count) => UnsafeUtils.MarshalTArray<T>(source.ReadBytes(length), 0, count);
        //public static void ReadTMany<T>(this BinaryReader source, T[] dest, int length) => UnsafeUtils.MarshalTArray<T>((FileStream)source.BaseStream, dest, 0, length);

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