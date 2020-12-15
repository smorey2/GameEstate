using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.FileTypes
{
    [PakFileType(PakFileType.QualityFilter)]
    public class QualityFilter : AbstractFileType, IGetExplorerInfo
    {
        public readonly uint[] IntStatFilter;
        public readonly uint[] Int64StatFilter;
        public readonly uint[] BoolStatFilter;
        public readonly uint[] FloatStatFilter;
        public readonly uint[] DidStatFilter;
        public readonly uint[] IidStatFilter;
        public readonly uint[] StringStatFilter;
        public readonly uint[] PositionStatFilter;
        public readonly uint[] AttributeStatFilter;
        public readonly uint[] Attribute2ndStatFilter;
        public readonly uint[] SkillStatFilter;

        public QualityFilter(BinaryReader r)
        {
            Id = r.ReadUInt32();
            var numInt = r.ReadUInt32();
            var numInt64 = r.ReadUInt32();
            var numBool = r.ReadUInt32();
            var numFloat = r.ReadUInt32();
            var numDid = r.ReadUInt32();
            var numIid = r.ReadUInt32();
            var numString = r.ReadUInt32();
            var numPosition = r.ReadUInt32();
            IntStatFilter = r.ReadTArray<uint>(sizeof(uint), (int)numInt);
            Int64StatFilter = r.ReadTArray<uint>(sizeof(uint), (int)numInt64);
            BoolStatFilter = r.ReadTArray<uint>(sizeof(uint), (int)numBool);
            FloatStatFilter = r.ReadTArray<uint>(sizeof(uint), (int)numFloat);
            DidStatFilter = r.ReadTArray<uint>(sizeof(uint), (int)numDid);
            IidStatFilter = r.ReadTArray<uint>(sizeof(uint), (int)numIid);
            StringStatFilter = r.ReadTArray<uint>(sizeof(uint), (int)numString);
            PositionStatFilter = r.ReadTArray<uint>(sizeof(uint), (int)numPosition);
            var numAttribute = r.ReadUInt32();
            var numAttribute2nd = r.ReadUInt32();
            var numSkill = r.ReadUInt32();
            AttributeStatFilter = r.ReadTArray<uint>(sizeof(uint), (int)numAttribute);
            Attribute2ndStatFilter = r.ReadTArray<uint>(sizeof(uint), (int)numAttribute2nd);
            SkillStatFilter = r.ReadTArray<uint>(sizeof(uint), (int)numSkill);
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(QualityFilter)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                })
            };
            return nodes;
        }
    }
}
