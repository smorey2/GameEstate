using System.Collections.Generic;

namespace GameEstate.Explorer.ViewModel
{
    public class ExplorerInfoNode
    {
        public string Name { get; set; }
        public List<ExplorerInfoNode> Items { get; set; }

        public ExplorerInfoNode(string name = "")
        {
            Name = name;
            Items = new List<ExplorerInfoNode>();
        }
    }
}
