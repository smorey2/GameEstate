namespace GameEstate.Formats.Valve.Blocks
{
    /// <summary>
    /// "AGRP" block.
    /// </summary>
    public class AGRP : DATABinaryKV3OrNTRO
    {
        public AGRP() : base("AnimationGroupResourceData_t") { }
    }
}
