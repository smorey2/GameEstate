using GameEstate.Core;

namespace GameEstate.Formats.Valve.Blocks
{
    public class REDIAdditionalInputDependencies : REDIInputDependencies
    {
        public override void WriteText(IndentedTextWriter w)
        {
            w.WriteLine($"Struct m_AdditionalInputDependencies[{List.Count}] = [");
            WriteList(w);
        }
    }
}
