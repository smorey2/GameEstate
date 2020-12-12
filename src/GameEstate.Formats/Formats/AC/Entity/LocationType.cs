using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class LocationType
    {
        public readonly int PartId;
        public readonly Frame Frame;

        public LocationType(BinaryReader r)
        {
            PartId = r.ReadInt32();
            Frame = new Frame(r);
        }
    }
}
