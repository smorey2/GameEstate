using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Props;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameEstate.Formats.AC.FileTypes
{
    [PakFileType(PakFileType.EnumMapper)]
    public class EnumMapper : AbstractFileType, IGetExplorerInfo
    {
        public readonly uint BaseEnumMap; // _base_emp_did
        public readonly NumberingType NumberingType;
        public readonly Dictionary<uint, string> IdToStringMap; // _id_to_string_map

        public EnumMapper(BinaryReader r)
        {
            Id = r.ReadUInt32();
            BaseEnumMap = r.ReadUInt32();
            NumberingType = (NumberingType)r.ReadByte();
            IdToStringMap = r.ReadC32Many<uint, string>(sizeof(uint), x => x.ReadL8ANSI(Encoding.Default));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(EnumMapper)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                    BaseEnumMap != 0 ? new ExplorerInfoNode($"BaseEnumMap: {BaseEnumMap:X8}") : null,
                    NumberingType != NumberingType.Undefined ? new ExplorerInfoNode($"NumberingType: {NumberingType}") : null,
                    IdToStringMap.Count > 0 ? new ExplorerInfoNode("IdToStringMap", items: IdToStringMap.OrderBy(x => x.Key).Select(x => new ExplorerInfoNode($"{x.Key}: {x.Value::X8}"))) : null,
                })
            };
            return nodes;
        }
    }
}
