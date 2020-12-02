using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats.Binary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEstate.Formats.BioWare
{
    public class BinaryPak : IDisposable, IGetExplorerInfo
    {
        public BinaryPak() { }
        public BinaryPak(BinaryReader r) => Read(r);

        public void Dispose()
        {
            Reader?.Dispose();
            Reader = null;
        }

        public enum DataType
        {
            GFF = 0,
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode("BinaryPak", items: new List<ExplorerInfoNode> {
                    new ExplorerInfoNode($"FileSize: {FileSize}"),
                    new ExplorerInfoNode($"Type: {Type}"),
                })
            };
            if (!nodes.Any(x => (x.Tag as ExplorerContentTab)?.Dispose != null))
                Dispose();
            return nodes;
        }

        public BinaryReader Reader { get; private set; }

        public uint FileSize { get; private set; }

        public DataType Type { get; private set; }

        public void Read(BinaryReader r)
        {
            Reader = r;
        }
    }
}
