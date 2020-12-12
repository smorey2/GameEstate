using GameEstate.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    /// <summary>
    /// A list of indexed vertices, and their associated type
    /// </summary>
    public class CVertexArray
    {
        public readonly int VertexType;
        public readonly Dictionary<ushort, SWVertex> Vertices;

        public CVertexArray(BinaryReader r)
        {
            VertexType = r.ReadInt32();
            var numVertices = r.ReadUInt32();
            if (VertexType == 1)
                Vertices = r.ReadTMany<ushort, SWVertex>(sizeof(ushort), x => new SWVertex(x), (int)numVertices);
            else throw new NotImplementedException();
        }
    }
}
