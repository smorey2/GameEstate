using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Entity;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.FileTypes
{
    /// <summary>
    /// These are client_portal.dat files starting with 0x0D. 
    /// These are basically pre-fab regions for things like the interior of a dungeon.
    /// </summary>
    [PakFileType(PakFileType.Environment)]
    public class Environment : AbstractFileType, IGetExplorerInfo
    {
        public readonly Dictionary<uint, CellStruct> Cells;

        public Environment(BinaryReader r)
        {
            Id = r.ReadUInt32(); // this will match fileId
            Cells = r.ReadL32Many<uint, CellStruct>(sizeof(uint), x => new CellStruct(x));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(Environment)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                    //new ExplorerInfoNode($"Type: {Type}"),
                })
            };
            return nodes;
        }
    }
}
