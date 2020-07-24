using System;
using System.IO;

namespace GameEstate.Formats.Valve.Blocks
{
    /// <summary>
    /// "SNAP" block.
    /// </summary>
    public class SNAP : Block
    {
        public override void Read(BinaryReader r, BinaryPak resource)
        {
            r.BaseStream.Position = Offset;
            throw new NotImplementedException();
        }
    }
}
