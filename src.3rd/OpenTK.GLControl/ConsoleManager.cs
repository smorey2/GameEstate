﻿using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace OpenTK
{
    [SuppressUnmanagedCodeSecurity]
    public static class ConsoleManager
    {
        const string Kernel32_DllName = "kernel32.dll";

        [DllImport(Kernel32_DllName)] static extern bool AllocConsole();
        [DllImport(Kernel32_DllName)] static extern bool FreeConsole();
        [DllImport(Kernel32_DllName)] static extern IntPtr GetConsoleWindow();
        [DllImport(Kernel32_DllName)] static extern int GetConsoleOutputCP();

        public static bool HasConsole => GetConsoleWindow() != IntPtr.Zero;

        /// <summary>
        /// Creates a new console instance if the process is not attached to a console already.
        /// </summary>
        public static void Show()
        {
            //#if DEBUG
            if (!HasConsole)
            {
                AllocConsole();
                //InvalidateOutAndError();
            }
            //#endif
        }

        /// <summary>
        /// If the process has a console attached to it, it will be detached and no longer visible. Writing to the System.Console is still possible, but no output will be shown.
        /// </summary>
        public static void Hide()
        {
            //#if DEBUG
            if (HasConsole)
            {
                SetOutAndErrorNull();
                FreeConsole();
            }
            //#endif
        }

        public static void Toggle()
        {
            if (HasConsole) Hide();
            else Show();
        }

        //static void InvalidateOutAndError()
        //{
        //    var type = typeof(Console);
        //    var _out = type.GetField("_out", BindingFlags.Static | BindingFlags.NonPublic);
        //    var _error = type.GetField("_error", BindingFlags.Static | BindingFlags.NonPublic);
        //    var _InitializeStdOutError = type.GetMethod("InitializeStdOutError", BindingFlags.Static | BindingFlags.NonPublic);

        //    Debug.Assert(_out != null);
        //    Debug.Assert(_error != null);
        //    Debug.Assert(_InitializeStdOutError != null);

        //    _out.SetValue(null, null);
        //    _error.SetValue(null, null);

        //    _InitializeStdOutError.Invoke(null, new object[] { true });
        //}

        static void SetOutAndErrorNull()
        {
            Console.SetOut(TextWriter.Null);
            Console.SetError(TextWriter.Null);
        }
    }
}

