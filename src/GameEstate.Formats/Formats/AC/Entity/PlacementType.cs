using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class PlacementType : IGetExplorerInfo
    {
        public readonly AnimationFrame AnimFrame;

        public PlacementType(BinaryReader r, uint numParts)
        {
            AnimFrame = new AnimationFrame(r, numParts);
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag) => (AnimFrame as IGetExplorerInfo).GetInfoNodes(resource, file, tag);
    }
}
