using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class LandDefs
    {
        public readonly int NumBlockLength;
        public readonly int NumBlockWidth;
        public readonly float SquareLength;
        public readonly int LBlockLength;
        public readonly int VertexPerCell;
        public readonly float MaxObjHeight;
        public readonly float SkyHeight;
        public readonly float RoadWidth;
        public readonly float[] LandHeightTable;

        public LandDefs(BinaryReader r)
        {
            NumBlockLength = r.ReadInt32();
            NumBlockWidth = r.ReadInt32();
            SquareLength = r.ReadSingle();
            LBlockLength = r.ReadInt32();
            VertexPerCell = r.ReadInt32();
            MaxObjHeight = r.ReadSingle();
            SkyHeight = r.ReadSingle();
            RoadWidth = r.ReadSingle();
            LandHeightTable = r.ReadTArray(x => x.ReadSingle(), 256);
        }
    }
}
