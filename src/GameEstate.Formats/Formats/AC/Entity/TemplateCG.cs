using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class TemplateCG
    {
        public string Name;
        public uint IconImage;
        public uint Title;
        // Attributes
        public uint Strength;
        public uint Endurance;
        public uint Coordination;
        public uint Quickness;
        public uint Focus;
        public uint Self;
        public uint[] NormalSkillsList;
        public uint[] PrimarySkillsList;

        public TemplateCG(BinaryReader r)
        {
            Name = r.ReadString();
            IconImage = r.ReadUInt32();
            Title = r.ReadUInt32();
            // Attributes
            Strength = r.ReadUInt32();
            Endurance = r.ReadUInt32();
            Coordination = r.ReadUInt32();
            Quickness = r.ReadUInt32();
            Focus = r.ReadUInt32();
            Self = r.ReadUInt32();
            NormalSkillsList = r.ReadC32Array<uint>(sizeof(uint));
            PrimarySkillsList = r.ReadC32Array<uint>(sizeof(uint));
        }
    }
}
