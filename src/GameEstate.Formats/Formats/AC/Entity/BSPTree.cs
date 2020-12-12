using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class BSPTree
    {
        public readonly BSPNode RootNode;

        public BSPTree(BinaryReader r, BSPType treeType)
        {
            RootNode = BSPNode.Factory(r, treeType);
        }
    }
}
