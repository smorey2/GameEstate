using GameEstate.Core;
using System.IO;
using System.Linq;

namespace GameEstate.Formats.AC.Entity
{
    public class Polygon
    {
        public readonly byte NumPts;
        public readonly StipplingType Stippling; // Whether it has that textured/bumpiness to it

        public readonly CullMode SidesType;
        public readonly short PosSurface;
        public readonly short NegSurface;

        public readonly short[] VertexIds;

        public readonly byte[] PosUVIndices;
        public readonly byte[] NegUVIndices;

        public SWVertex[] Vertices;

        public Polygon(BinaryReader r)
        {
            NumPts = r.ReadByte();
            Stippling = (StipplingType)r.ReadByte();

            SidesType = (CullMode)r.ReadInt32();
            PosSurface = r.ReadInt16();
            NegSurface = r.ReadInt16();

            VertexIds = r.ReadTArray<short>(sizeof(short), NumPts);

            if (!Stippling.HasFlag(StipplingType.NoPos))
                PosUVIndices = r.ReadBytes(NumPts);
            if (SidesType == CullMode.Clockwise && !Stippling.HasFlag(StipplingType.NoNeg))
                NegUVIndices = r.ReadBytes(NumPts);

            if (SidesType == CullMode.None)
            {
                NegSurface = PosSurface;
                NegUVIndices = PosUVIndices;
            }
        }

        public void LoadVertices(CVertexArray vertexArray) => Vertices = VertexIds.Select(id => vertexArray.Vertices[(ushort)id]).ToArray();
    }
}
