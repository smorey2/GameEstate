using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Props;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameEstate.Formats.AC.FileTypes
{
    /// <summary>
    /// EnumMapper files are 0x27 in the client_portal.dat
    /// They contain a list of Weenie IDs and their W_Class. The client uses these for items such as tracking spell components (to know if the player has all required to cast a spell).
    ///
    /// A description of each DualDidMapper is in DidMapper entry 0x25000005 (WEENIE_CATEGORIES)
    /// 27000000 - Materials
    /// 27000001 - Gems
    /// 27000002 - SpellComponents
    /// 27000003 - ComponentPacks
    /// 27000004 - TradeNotes
    /// </summary>
    [PakFileType(PakFileType.DualDidMapper)]
    public class DualDidMapper : AbstractFileType, IGetExplorerInfo
    {
        // The client/server designation is guessed based on the content in each list.
        // The keys in these two Dictionaries are common. So ClientEnumToId[key] = ClientEnumToName[key].
        public readonly NumberingType ClientIDNumberingType; // bitfield designating how the numbering is organized. Not really needed for our usage.
        public readonly Dictionary<uint, uint> ClientEnumToID; // _EnumToID
        
        public readonly NumberingType ClientNameNumberingType; // bitfield designating how the numbering is organized. Not really needed for our usage.
        public readonly Dictionary<uint, string> ClientEnumToName = new Dictionary<uint, string>(); // _EnumToName
        
        // The keys in these two Dictionaries are common. So ServerEnumToId[key] = ServerEnumToName[key].
        public readonly NumberingType ServerIDNumberingType; // bitfield designating how the numbering is organized. Not really needed for our usage.
        public readonly Dictionary<uint, uint> ServerEnumToID; // _EnumToID
        
        public readonly NumberingType ServerNameNumberingType; // bitfield designating how the numbering is organized. Not really needed for our usage.
        public readonly Dictionary<uint, string> ServerEnumToName; // _EnumToName

        public DualDidMapper(BinaryReader r)
        {
            Id = r.ReadUInt32();
            
            ClientIDNumberingType = (NumberingType)r.ReadByte();
            ClientEnumToID = r.ReadC32Many<uint, uint>(sizeof(uint), x => x.ReadUInt32());
            
            ClientNameNumberingType = (NumberingType)r.ReadByte();
            ClientEnumToName = r.ReadC32Many<uint, string>(sizeof(uint), x => x.ReadL8ANSI(Encoding.Default));
            
            ServerIDNumberingType = (NumberingType)r.ReadByte();
            ServerEnumToID = r.ReadC32Many<uint, uint>(sizeof(uint), x => x.ReadUInt32());
            
            ServerNameNumberingType = (NumberingType)r.ReadByte();
            ServerEnumToName = r.ReadC32Many<uint, string>(sizeof(uint), x => x.ReadL8ANSI(Encoding.Default));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(DualDidMapper)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                    //new ExplorerInfoNode($"Type: {Type}"),
                })
            };
            return nodes;
        }
    }
}
