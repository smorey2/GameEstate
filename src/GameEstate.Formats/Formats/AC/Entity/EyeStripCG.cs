using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class EyeStripCG : IGetExplorerInfo
    {
        public readonly uint IconImage;
        public readonly uint IconImageBald;
        public readonly ObjDesc ObjDesc;
        public readonly ObjDesc ObjDescBald;

        public EyeStripCG(BinaryReader r)
        {
            IconImage = r.ReadUInt32();
            IconImageBald = r.ReadUInt32();
            ObjDesc = new ObjDesc(r);
            ObjDescBald = new ObjDesc(r);
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                IconImage != 0 ? new ExplorerInfoNode($"Icon: {IconImage:X8}") : null,
                IconImageBald != 0 ? new ExplorerInfoNode($"Bald Icon: {IconImageBald:X8}") : null,
                new ExplorerInfoNode("ObjDesc", items: (ObjDesc as IGetExplorerInfo).GetInfoNodes()),
                new ExplorerInfoNode("ObjDescBald", items: (ObjDescBald as IGetExplorerInfo).GetInfoNodes()),
            };
            return nodes;
        }
    }
}
