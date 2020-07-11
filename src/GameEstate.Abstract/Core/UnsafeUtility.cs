using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace GameEstate.Core
{
    public static class UnsafeUtils
    {
        static UnsafeUtils() => EstateBootstrap.Touch();

        [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)] extern static unsafe IntPtr memcpy(IntPtr dest, IntPtr src, uint count);
        public static Func<IntPtr, IntPtr, uint, IntPtr> Memcpy = memcpy;

        public static unsafe string ReadZASCII(byte* name, int length)
        {
            var i = 0;
            while (name[i] != 0 && length-- > 0) i++;
            if (i == 0)
                return null;
            var name2 = new byte[i];
            fixed (byte* pB = name2)
                while (--i >= 0)
                    pB[i] = name[i];
            return Encoding.ASCII.GetString(name2);
        }

        [DllImport("Kernel32")] extern static unsafe int _lread(SafeFileHandle hFile, void* lpBuffer, int wBytes);
        public static unsafe void ReadBuffer(this FileStream stream, byte[] buf, int length)
        {
            if (EstatePlatform.Platform == EstatePlatform.PlatformWindows)
                fixed (byte* pbuf = buf)
                    _lread(stream.SafeFileHandle, pbuf, length);
            else
                stream.Read(buf, 0, length);
        }

        public static unsafe T MarshalT<T>(byte[] bytes, int length = -1)
        {
            var size = Marshal.SizeOf(typeof(T));
            if (length > 0 && size > length)
                Array.Resize(ref bytes, size);
            fixed (byte* src = bytes)
                return (T)Marshal.PtrToStructure(new IntPtr(src), typeof(T));
            //fixed (byte* src = bytes)
            //{
            //    var r = default(T);
            //    var hr = GCHandle.Alloc(r, GCHandleType.Pinned);
            //    Memcpy(hr.AddrOfPinnedObject(), new IntPtr(src + offset), (uint)bytes.Length);
            //    hr.Free();
            //    return r;
            //}
        }

        public static void MarshalTArray<T>(FileStream stream, T[] dest, int offset, int length)
        {
            var h = GCHandle.Alloc(dest, GCHandleType.Pinned);
#if !MONO
            NativeReader.Read(stream.SafeFileHandle.DangerousGetHandle() + offset, h.AddrOfPinnedObject(), length);
#else
            NativeReader.Read(stream.Handle + offset, pArray, length);
#endif
            h.Free();
        }

        public static unsafe T[] MarshalTArray<T>(byte[] bytes, int offset, int count)
        {
            var result = new T[count];
            fixed (byte* src = bytes)
            {
                var hresult = GCHandle.Alloc(result, GCHandleType.Pinned);
                Memcpy(hresult.AddrOfPinnedObject(), new IntPtr(src + offset), (uint)bytes.Length);
                hresult.Free();
                return result;
            }
        }

        public static short Reverse(short value) => (short)(
                ((value & 0xFF00) >> 8) << 0 |
                ((value & 0x00FF) >> 0) << 8);
        public static ushort Reverse(ushort value) => (ushort)(
                ((value & 0xFF00) >> 8) << 0 |
                ((value & 0x00FF) >> 0) << 8);
        public static int Reverse(int value) => (int)(
                (((uint)value & 0xFF000000) >> 24) << 0 |
                (((uint)value & 0x00FF0000) >> 16) << 8 |
                (((uint)value & 0x0000FF00) >> 8) << 16 |
                (((uint)value & 0x000000FF) >> 0) << 24);
        public static uint Reverse(uint value) => (uint)(
                ((value & 0xFF000000) >> 24) << 0 |
                ((value & 0x00FF0000) >> 16) << 8 |
                ((value & 0x0000FF00) >> 8) << 16 |
                ((value & 0x000000FF) >> 0) << 24);
        public static long Reverse(long value) => (long)(
                (((ulong)value & 0xFF00000000000000UL) >> 56) << 0 |
                (((ulong)value & 0x00FF000000000000UL) >> 48) << 8 |
                (((ulong)value & 0x0000FF0000000000UL) >> 40) << 16 |
                (((ulong)value & 0x000000FF00000000UL) >> 32) << 24 |
                (((ulong)value & 0x00000000FF000000UL) >> 24) << 32 |
                (((ulong)value & 0x0000000000FF0000UL) >> 16) << 40 |
                (((ulong)value & 0x000000000000FF00UL) >> 8) << 48 |
                (((ulong)value & 0x00000000000000FFUL) >> 0) << 56);
        public static ulong Reverse(ulong value) => (ulong)(
                ((value & 0xFF00000000000000UL) >> 56) << 0 |
                ((value & 0x00FF000000000000UL) >> 48) << 8 |
                ((value & 0x0000FF0000000000UL) >> 40) << 16 |
                ((value & 0x000000FF00000000UL) >> 32) << 24 |
                ((value & 0x00000000FF000000UL) >> 24) << 32 |
                ((value & 0x0000000000FF0000UL) >> 16) << 40 |
                ((value & 0x000000000000FF00UL) >> 8) << 48 |
                ((value & 0x00000000000000FFUL) >> 0) << 56);
    }
}