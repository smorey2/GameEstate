using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace GameEstate.Core
{
    public static class UnsafeUtils
    {
        static UnsafeUtils() => Estate.Bootstrap();

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
    }
}