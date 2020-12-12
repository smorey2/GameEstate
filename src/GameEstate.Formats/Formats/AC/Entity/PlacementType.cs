using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class PlacementType
    {
        public readonly AnimationFrame AnimFrame;

        public PlacementType(BinaryReader r, uint numParts)
        {
            AnimFrame = new AnimationFrame(r, numParts);
        }
    }
}
