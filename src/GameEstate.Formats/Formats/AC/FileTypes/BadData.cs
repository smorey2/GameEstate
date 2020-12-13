using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEstate.Formats.AC.FileTypes
{
    [PakFileType(PakFileType.BadData)]
    public class BadData : AbstractFileType, IGetExplorerInfo
    {
        public const uint FILE_ID = 0x0E00001A;

        // Key is a list of a WCIDs that are "bad" and should not exist. The value is always 1 (could be a bool?)
        public readonly Dictionary<uint, uint> Bad;

        public BadData(BinaryReader r)
        {
            Id = r.ReadUInt32();
            Bad = r.ReadL16Many<uint, uint>(sizeof(uint), x => x.ReadUInt32(), offset: 2);
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(BadData)}: {Id:X8}", items: Bad.Keys.OrderBy(x => x).Select(x => new ExplorerInfoNode($"{x}")))
            };
            return nodes;
        }
    }
}
