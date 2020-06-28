using GameEstate.Core;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameEstate.Explorer.ViewModel
{
    [DebuggerDisplay("{Name}, children: {Items.Count}")]
    public class ExplorerItemNode
    {
        public class Filter
        {
            public string Name;
            public string Description;

            public Filter(string name, string description = "")
            {
                Name = name;
                Description = description;
            }

            public override string ToString() => Name;
        }

        public AbstractPakFile PakFile { get; set; }
        public AbstractDatFile DatFile { get; set; }
        public object Icon { get; set; }
        public string Name { get; set; }
        public object Tag { get; set; }
        public List<ExplorerItemNode> Items { get; set; }

        public ExplorerItemNode(string name = "")
        {
            Name = name;
            Items = new List<ExplorerItemNode>();
        }
    }
}
