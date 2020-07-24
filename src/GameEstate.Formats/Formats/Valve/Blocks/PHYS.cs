namespace GameEstate.Formats.Valve.Blocks
{
    /// <summary>
    /// "PHYS" block.
    /// </summary>
    public class PHYS : DATABinaryKV3OrNTRO
    {
        public PHYS() : base("VPhysXAggregateData_t") { }
    }
}
