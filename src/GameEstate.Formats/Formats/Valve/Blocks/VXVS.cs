using System;
using System.IO;

namespace GameEstate.Formats.Valve.Blocks
{
    /// <summary>
    /// "VXVS" block.
    /// </summary>
    public class VXVS : Block
    {
        public override void Read(BinaryReader r, BinaryPak resource)
        {
            r.BaseStream.Position = Offset;
            throw new NotImplementedException();
        }
    }
}
