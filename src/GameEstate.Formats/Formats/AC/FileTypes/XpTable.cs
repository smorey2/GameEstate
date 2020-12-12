using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.FileTypes
{
    /// <summary>
    /// Reads and stores the XP Tables from the client_portal.dat (file 0x0E000018).
    /// </summary>
    [PakFileType(PakFileType.XpTable)]
    public class XpTable : FileType
    {
        public const uint FILE_ID = 0x0E000018;

        public List<uint> AttributeXpList { get; } = new List<uint>();
        public List<uint> VitalXpList { get; } = new List<uint>();
        public List<uint> TrainedSkillXpList { get; } = new List<uint>();
        public List<uint> SpecializedSkillXpList { get; } = new List<uint>();

        /// <summary>
        /// The XP needed to reach each level
        /// </summary>
        public List<ulong> CharacterLevelXPList { get; } = new List<ulong>();

        /// <summary>
        /// Number of credits gifted at each level
        /// </summary>
        public List<uint> CharacterLevelSkillCreditList { get; } = new List<uint>();

        public override void Read(BinaryReader r)
        {
            Id = r.ReadUInt32();
            // The counts for each "Table" are at the top of the file.
            var attributeCount = r.ReadInt32();
            var vitalCount = r.ReadInt32();
            var trainedSkillCount = r.ReadInt32();
            var specializedSkillCount = r.ReadInt32();
            var levelCount = r.ReadUInt32();
            for (var i = 0; i <= attributeCount; i++)
                AttributeXpList.Add(r.ReadUInt32());
            for (var i = 0; i <= vitalCount; i++)
                VitalXpList.Add(r.ReadUInt32());
            for (var i = 0; i <= trainedSkillCount; i++)
                TrainedSkillXpList.Add(r.ReadUInt32());
            for (var i = 0; i <= specializedSkillCount; i++)
                SpecializedSkillXpList.Add(r.ReadUInt32());
            for (var i = 0; i <= levelCount; i++)
                CharacterLevelXPList.Add(r.ReadUInt64());
            for (var i = 0; i <= levelCount; i++)
                CharacterLevelSkillCreditList.Add(r.ReadUInt32());
        }
    }
}
