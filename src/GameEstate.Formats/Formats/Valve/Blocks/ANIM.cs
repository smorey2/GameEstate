namespace GameEstate.Formats.Valve.Blocks
{
    /// <summary>
    /// "ANIM" block.
    /// </summary>
    public class ANIM : DATABinaryKV3OrNTRO
    {
        public ANIM() : base("AnimationResourceData_t") { }
    }
}
