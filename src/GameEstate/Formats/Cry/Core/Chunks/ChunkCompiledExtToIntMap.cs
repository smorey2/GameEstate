﻿using static GameEstate.EstateDebug;

namespace GameEstate.Formats.Cry.Core
{
    public abstract class ChunkCompiledExtToIntMap : Chunk
    {
        public int Reserved;
        public uint NumExtVertices;
        public ushort[] Source;

        public override void WriteChunk()
        {
            Log($"*** START MorphTargets Chunk ***");
            Log($"    ChunkType:           {ChunkType}");
            Log($"    Node ID:             {ID:X}");
        }
    }
}
