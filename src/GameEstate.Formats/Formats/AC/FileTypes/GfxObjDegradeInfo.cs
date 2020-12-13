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
    /// These are client_portal.dat files starting with 0x11. 
    /// Contains info on what objects to display at what distance to help with render performance (e.g. low-poly very far away, but high-poly when close)
    /// </summary>
    [PakFileType(PakFileType.DegradeInfo)]
    public class GfxObjDegradeInfo : AbstractFileType, IGetExplorerInfo
    {
        public readonly GfxObjInfo[] Degrades;

        public GfxObjDegradeInfo(BinaryReader r)
        {
            Id = r.ReadUInt32();
            Degrades = r.ReadL32Array(x => new GfxObjInfo(x));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(GfxObjDegradeInfo)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                    //new ExplorerInfoNode($"Type: {Type}"),
                })
            };
            return nodes;
        }
    }
}
