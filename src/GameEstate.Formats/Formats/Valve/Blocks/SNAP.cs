using GameEstate.Core;
using System;
using System.IO;

namespace GameEstate.Formats.Valve.Blocks
{
    /// <summary>
    /// "SNAP" block.
    /// </summary>
    public class SNAP : Block
    {
        public override void Read(BinaryPak parent, BinaryReader r)
        {
            r.Position(Offset);
            throw new NotImplementedException();
        }
    }
}
