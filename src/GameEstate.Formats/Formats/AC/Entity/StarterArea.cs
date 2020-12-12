using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class StarterArea
    {
        public readonly string Name;
        public readonly Position[] Locations;

        public StarterArea(BinaryReader r)
        {
            Name = r.ReadString();
            Locations = r.ReadC32Array(x => new Position(x));
        }
    }
}
