﻿using static GameEstate.EstateDebug;

namespace GameEstate.Formats.Cry.Core
{
    public abstract class ChunkCompiledMorphTargets : Chunk
    {
        public uint NumberOfMorphTargets;
        public MeshMorphTargetVertex[] MorphTargetVertices;

        public override void WriteChunk()
        {
            Log($"*** START MorphTargets Chunk ***");
            Log($"    ChunkType:           {ChunkType}");
            Log($"    Node ID:             {ID:X}");
            Log($"    Number of Targets:   {NumberOfMorphTargets:X}");
        }
    }
}
