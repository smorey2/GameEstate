﻿using static GameEstate.Core.CoreDebug;

namespace GameEstate.Formats.Cry.Core
{
    /// <summary>
    /// Legacy class.  No longer used.
    /// </summary>
    public abstract class ChunkMeshMorphTargets : Chunk
    {
        public uint ChunkIDMesh;
        public uint NumMorphVertices;

        public override void WriteChunk()
        {
            Log($"*** START MorphTargets Chunk ***");
            Log($"    ChunkType:           {ChunkType}");
            Log($"    Node ID:             {ID:X}");
            Log($"    Chunk ID Mesh:       {ChunkIDMesh:X}");
        }
    }
}