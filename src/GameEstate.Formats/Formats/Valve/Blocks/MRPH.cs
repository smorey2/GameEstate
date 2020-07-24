namespace GameEstate.Formats.Valve.Blocks
{
    /// <summary>
    /// "MRPH" block.
    /// </summary>
    public class MRPH : DATABinaryKV3OrNTRO
    {
        public MRPH() : base("MorphSetData_t") { }
    }
}
