using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.Rsi
{
    public class DatabaseFile : IDisposable, IGetExplorerInfo
    {
        public DatabaseFile() { }
        public DatabaseFile(BinaryReader r) => Read(r);

        public void Dispose()
        {
            Reader?.Dispose();
            Reader = null;
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode("DatabasePak", items: new List<ExplorerInfoNode> {
                    new ExplorerInfoNode($"FileSize: [FileSize]"),
                })
            };
            return nodes;
        }

        public BinaryReader Reader { get; private set; }

        public void Read(BinaryReader r)
        {
            Reader = r;
        }
    }
}
