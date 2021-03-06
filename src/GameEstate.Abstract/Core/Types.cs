﻿using System.Numerics;
using System.Runtime.InteropServices;

namespace GameEstate.Core
{
    // MARK Vector2

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Int2 { public int X; public int Y; public override string ToString() => $"{X},{Y}"; }

    // MARK Vector3

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Int3 { public int X; public int Y; public int Z; public Int3(int x, int y, int z) { X = x; Y = y; Z = z; } public override string ToString() => $"{X},{Y},{Z}"; }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Float3 { public float X; public float Y; public float Z; public override string ToString() => $"{X},{Y},{Z}"; public Vector3 ToVector3() => new Vector3(X, Y, Z); }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Byte3 { public byte X; public byte Y; public byte Z; public override string ToString() => $"{X},{Y},{Z}"; }
}