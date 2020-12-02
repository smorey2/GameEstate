using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats.Binary;
//using GameEstate.Formats.Red.Blocks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace GameEstate.Formats.Red
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
            GFF,
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

        //public List<Block> Blocks { get; } = new List<Block>();
        
        // Headers
        #region Headers

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct GFF_Header
        {
            public uint Type;           // Version ("DLG ")
            public uint Version;        // Version ("V3.3")
            public uint StructOffset;           // Number of entries in FILETABLE
            public uint Structs;            // Number of entries in KEYTABLE.
            public uint FieldOffset;          // Not used
            public uint Fields;      // Offset to FILETABLE (0x440000).
            public uint LabelOffset;       // Offset to KEYTABLE.
            public uint Labels;          // Build year (less 1900).
            public uint FieldDataOffset;           // Build day
            public uint FieldDatas;           // Build day
            public uint FieldIndicesOffset;           // Build day
            public uint FieldIndices;           // Build day
            public uint ListIndicesOffset;           // Build day
            public uint ListIndices;           // Build day
        }

        #endregion

        public void Read(BinaryReader r)
        {
            Reader = r;
        }
    }
}
