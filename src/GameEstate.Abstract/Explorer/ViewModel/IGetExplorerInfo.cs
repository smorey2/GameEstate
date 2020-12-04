using GameEstate.Formats._Packages;
using System.Collections.Generic;

namespace GameEstate.Explorer.ViewModel
{
    public interface IGetExplorerInfo
    {
        List<ExplorerInfoNode> GetInfoNodes(ExplorerManager resource, FileMetadata file);
    }
}
