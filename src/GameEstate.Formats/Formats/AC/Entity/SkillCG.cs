using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class SkillCG
    {
        public readonly uint SkillNum;
        public readonly int NormalCost;
        public readonly int PrimaryCost;

        public SkillCG(BinaryReader r)
        {
            SkillNum = r.ReadUInt32();
            NormalCost = r.ReadInt32();
            PrimaryCost = r.ReadInt32();
        }
    }
}
