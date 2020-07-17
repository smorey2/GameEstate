using GameEstate.Core;
using System;
using System.IO;

namespace GameEstate.Formats.Valve.Blocks
{
    public class REDICustomDependencies : REDIAbstract
    {
        public override void Read(BinaryReader r, BinaryPak resource)
        {
            r.BaseStream.Position = Offset;
            if (Size > 0)
                throw new NotImplementedException("CustomDependencies block is not handled.");
        }

        public override void WriteText(IndentedTextWriter w)
        {
            w.WriteLine($"Struct m_CustomDependencies[{0}] = ["); w.Indent++;
            w.Indent--; w.WriteLine("]");
        }
    }
}
