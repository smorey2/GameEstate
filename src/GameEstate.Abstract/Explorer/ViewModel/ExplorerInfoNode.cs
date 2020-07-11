using System.Collections.Generic;

namespace GameEstate.Explorer.ViewModel
{
    public class ExplorerInfoNode
    {
        public string Name { get; set; }
        public object Tag { get; }
        public List<ExplorerInfoNode> Items { get; }

        public ExplorerInfoNode(string name, object tag = null, List<ExplorerInfoNode> items = null)
        {
            Name = name;
            Tag = tag;
            Items = items ?? new List<ExplorerInfoNode>();
        }
    }
}
