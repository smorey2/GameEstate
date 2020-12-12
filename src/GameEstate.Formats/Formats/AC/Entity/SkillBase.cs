using GameEstate.Core;
using System;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class SkillBase
    {
        public readonly string Description;
        public readonly string Name;
        public readonly uint IconId;
        public readonly int TrainedCost;
        /// <summary>
        /// This is the total cost to specialize a skill, which INCLUDES the trained cost.
        /// </summary>
        public readonly int SpecializedCost;
        public readonly uint Category;      // 1 = combat, 2 = other, 3 = magic
        public readonly uint ChargenUse;    // always 1?
        /// <summary>
        /// This is the minimum SAC required for usability.
        /// 1 = Usable when untrained
        /// 2 = Trained or greater required for usability
        /// </summary>
        public readonly uint MinLevel;      // 1-2?

        public readonly SkillFormula Formula;

        public readonly double UpperBound;
        public readonly double LowerBound;
        public readonly double LearnMod;

        public int UpgradeCostFromTrainedToSpecialized => SpecializedCost - TrainedCost;

        public SkillBase() { }
        public SkillBase(SkillFormula formula) => Formula = formula;
        public SkillBase(BinaryReader r)
        {
            Description = r.ReadL16String(Encoding.Default); r.AlignBoundary();
            Name = r.ReadL16String(Encoding.Default); r.AlignBoundary();
            IconId = r.ReadUInt32();
            TrainedCost = r.ReadInt32();
            SpecializedCost = r.ReadInt32();
            Category = r.ReadUInt32();
            ChargenUse = r.ReadUInt32();
            MinLevel = r.ReadUInt32();
            Formula = new SkillFormula(r);
            UpperBound = r.ReadDouble();
            LowerBound = r.ReadDouble();
            LearnMod = r.ReadDouble();
        }
    }
}
