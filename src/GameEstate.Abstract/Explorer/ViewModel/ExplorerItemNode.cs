﻿using GameEstate.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameEstate.Explorer.ViewModel
{
    [DebuggerDisplay("{Name}, items: {Items.Count}")]
    public class ExplorerItemNode
    {
        [DebuggerDisplay("{Name}")]
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
        public List<ExplorerItemNode> Items { get; private set; }
        public AbstractPakFile PakFile { get; set; }

        public ExplorerItemNode(string name, object icon, object tag = null, List<ExplorerItemNode> children = null)
        {
            Name = name;
            Icon = icon;
            Tag = tag;
            Items = children ?? new List<ExplorerItemNode>();
        }

        public ExplorerItemNode Search(Func<ExplorerItemNode, bool> predicate)
        {
            // if node is a leaf
            if (Items == null || Items.Count == 0)
                return predicate(this) ? this : null;
            // Otherwise if node is not a leaf
            else
            {
                var results = Items.Select(i => i.Search(predicate)).Where(i => i != null).ToList();
                if (results.Any())
                {
                    var result = (ExplorerItemNode)MemberwiseClone();
                    result.Items = results;
                    return result;
                }
                return null;
            }
        }
    }
}
