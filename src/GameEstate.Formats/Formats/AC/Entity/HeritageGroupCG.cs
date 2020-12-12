using GameEstate.Core;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class HeritageGroupCG
    {
        public readonly string Name;
        public readonly uint IconImage;
        public readonly uint SetupID; // Basic character model
        public readonly uint EnvironmentSetupID; // This is the background environment during Character Creation
        public readonly uint AttributeCredits;
        public readonly uint SkillCredits;
        public readonly int[] PrimaryStartAreas;
        public readonly int[] SecondaryStartAreas;
        public readonly SkillCG[] Skills;
        public readonly TemplateCG[] Templates;
        public readonly Dictionary<int, SexCG> Genders;

        public HeritageGroupCG(BinaryReader r)
        {
            Name = r.ReadString();
            IconImage = r.ReadUInt32();
            SetupID = r.ReadUInt32();
            EnvironmentSetupID = r.ReadUInt32();
            AttributeCredits = r.ReadUInt32();
            SkillCredits = r.ReadUInt32();

            PrimaryStartAreas = r.ReadC32Array<int>(sizeof(int));
            SecondaryStartAreas = r.ReadC32Array<int>(sizeof(int));
            Skills = r.ReadC32Array(x => new SkillCG(x));
            Templates = r.ReadC32Array(x => new TemplateCG(x));
            r.BaseStream.Position++; // 0x01 byte here. Not sure what/why, so skip it!
            Genders = r.ReadC32Many<int, SexCG>(sizeof(int), x => new TemplateCG(x));
        }
    }
}
