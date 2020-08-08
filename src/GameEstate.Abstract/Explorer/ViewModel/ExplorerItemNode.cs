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

        public string Name { get; }
        public object Icon { get; }
        public object Tag { get; }
        public List<ExplorerItemNode> Items { get; }
        public AbstractPakFile PakFile { get; set; }

        public ExplorerItemNode(string name, object icon, object tag = null, List<ExplorerItemNode> items = null)
        {
            Name = name;
            Icon = icon;
            Tag = tag;
            Items = items ?? new List<ExplorerItemNode>();
        }
    }
}
