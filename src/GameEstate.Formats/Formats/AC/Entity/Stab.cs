using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    /// <summary>
    /// I'm not quite sure what a "Stab" is, but this is what the client calls these.
    /// It is an object and a corresponding position. 
    /// Note that since these are referenced by either a landblock or a cellblock, the corresponding Landblock and Cell should come from the parent.
    /// </summary>
    public class Stab
    {
        public readonly uint Id;
        public readonly Frame Frame;

        public Stab(BinaryReader r)
        {
            Id = r.ReadUInt32();
            Frame.Unpack(r);
        }
    }
}
