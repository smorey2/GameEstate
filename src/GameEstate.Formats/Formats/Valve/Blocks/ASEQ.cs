namespace GameEstate.Formats.Valve.Blocks
{
    /// <summary>
    /// "ASEQ" block.
    /// </summary>
    public class ASEQ : DATABinaryKV3OrNTRO
    {
        public ASEQ() : base("SequenceGroupResourceData_t") { }
    }
}
