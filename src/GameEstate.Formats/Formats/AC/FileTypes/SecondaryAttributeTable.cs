using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Entity;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.FileTypes
{
    [PakFileType(PakFileType.SecondaryAttributeTable)]
    public class SecondaryAttributeTable : AbstractFileType, IGetExplorerInfo
    {
        public const uint FILE_ID = 0x0E000003;

        public readonly Attribute2ndBase MaxHealth;
        public readonly Attribute2ndBase MaxStamina;
        public readonly Attribute2ndBase MaxMana;

        public SecondaryAttributeTable(BinaryReader r)
        {
            Id = r.ReadUInt32();
            MaxHealth = new Attribute2ndBase(r);
            MaxStamina = new Attribute2ndBase(r);
            MaxMana = new Attribute2ndBase(r);
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(SecondaryAttributeTable)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                })
            };
            return nodes;
        }
    }
}
