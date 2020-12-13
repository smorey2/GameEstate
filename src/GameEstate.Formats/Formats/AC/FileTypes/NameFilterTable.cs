using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Entity;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.FileTypes
{
    [PakFileType(PakFileType.NameFilterTable)]
    public class NameFilterTable : AbstractFileType, IGetExplorerInfo
    {
        public const uint FILE_ID = 0x0E000020;

        // Key is a list of a WCIDs that are "bad" and should not exist. The value is always 1 (could be a bool?)
        public readonly Dictionary<uint, NameFilterLanguageData> LanguageData;

        public NameFilterTable(BinaryReader r)
        {
            Id = r.ReadUInt32();
            LanguageData = r.ReadL16Many<uint, NameFilterLanguageData>(sizeof(uint), x => new NameFilterLanguageData(x));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(NameFilterTable)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                    //new ExplorerInfoNode($"Type: {Type}"),
                })
            };
            return nodes;
        }
    }
}
