﻿using System.Collections.Generic;
using static GameEstate.EstateDebug;

namespace GameEstate.Formats.Cry.Core
{
    /// <summary>
    /// Legacy class. Not used
    /// </summary>
    public abstract class ChunkBoneNameList : Chunk
    {
        public int NumEntities;
        public List<string> BoneNames;

        public override void WriteChunk()
        {
            Log($"*** START MorphTargets Chunk ***");
            Log($"    ChunkType:           {ChunkType}");
            Log($"    Node ID:             {ID:X}");
            Log($"    Number of Targets:   {NumEntities:X}");
            foreach (var name in BoneNames)
                Log($"    Bone Name:       {name}");
        }
    }
}
