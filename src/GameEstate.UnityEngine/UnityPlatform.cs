﻿using GameEstate.Core;
using System;
using System.Threading.Tasks;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace GameEstate
{
    public static class UnityPlatform
    {
        public static unsafe bool Startup()
        {
            var task = Task.Run(() => Application.platform.ToString());
            try
            {
                EstatePlatform.Platform = task.Result;
                //Debug.Log(Platform);
                UnsafeUtils.Memcpy = (dest, src, count) => { UnsafeUtility.MemCpy((void*)dest, (void*)src, count); return IntPtr.Zero; };
                EstateDebug.AssertFunc = x => Debug.Assert(x);
                EstateDebug.LogFunc = a => Debug.Log(a);
                EstateDebug.LogFormatFunc = (a, b) => Debug.LogFormat(a, b);
                return true;
            }
            catch { return false; }
        }
    }
}