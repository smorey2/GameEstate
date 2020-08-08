using System;
using System.Diagnostics;

namespace GameEstate.Explorer.ViewModel
{
    [DebuggerDisplay("{Name}")]
    public class ExplorerContentTab
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public Type EngineType { get; set; }
    }
}
