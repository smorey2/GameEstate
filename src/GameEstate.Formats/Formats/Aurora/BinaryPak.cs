using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats.Binary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace GameEstate.Formats.Aurora
{
    public class BinaryPak : IGetExplorerInfo
    {
        public BinaryPak() { }
        public BinaryPak(BinaryReader r) => Read(r);

        public enum DataType : uint
        {
            DLG = 0x20474c44,
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode("BinaryPak", items: new List<ExplorerInfoNode> {
                    new ExplorerInfoNode($"Type: {Type}"),
                })
            };
            return nodes;
        }

        public DataType Type { get; private set; }

        public IDictionary<string, object> Root { get; private set; }

        // Headers
        #region Headers

        const uint GFF_VERSION3_2 = 0x322e3356; // literal string "V3.2".
        const uint GFF_VERSION3_3 = 0x332e3356; // literal string "V3.3".

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct GFF_Header
        {
            public uint Type;               // File Type ("DLG ")
            public uint Version;            // GFF Version ("V3.3")
            public uint StructOffset;       // Offset of Struct array as bytes from the beginning of the file
            public uint StructCount;        // Number of elements in Struct array
            public uint FieldOffset;        // Offset of Field array as bytes from the beginning of the file
            public uint FieldCount;         // Number of elements in Field array
            public uint LabelOffset;        // Offset of Label array as bytes from the beginning of the file
            public uint LabelCount;         // Number of elements in Label array
            public uint FieldDataOffset;    // Offset of Field Data as bytes from the beginning of the file
            public uint FieldDataSize;      // Number of bytes in Field Data block
            public uint FieldIndicesOffset; // Offset of Field Indices array as bytes from the beginning of the file
            public uint FieldIndicesSize;   // Number of bytes in Field Indices array
            public uint ListIndicesOffset;  // Offset of List Indices array as bytes from the beginning of the file
            public uint ListIndicesSize;    // Number of bytes in List Indices array
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct GFF_Struct
        {
            public uint Type;               // Programmer-defined integer ID.
            public uint DataOrDataOffset;   // If FieldCount = 1, this is an index into the Field Array.
                                            // If FieldCount > 1, this is a byte offset into the Field Indices array.
            public uint FieldCount;         // Number of fields in this Struct.
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct GFF_Field
        {
            public uint Type;               // Data type
            public uint LabelIndex;         // Index into the Label Array
            public uint DataOrDataOffset;   // If Type is a simple data type, then this is the value actual of the field.
                                            // If Type is a complex data type, then this is a byte offset into the Field Data block.
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        unsafe struct GFF_Label
        {
            public fixed byte Name[0x10];     // Label
        }

        #endregion

        public class ResourceRef
        {
            public string Name;
        }

        public class LocalizedRef
        {
            public uint DialogID;
            public (uint id, string value)[] Values;
        }

        public unsafe void Read(BinaryReader r)
        {
            var header = r.ReadT<GFF_Header>(sizeof(GFF_Header));
            if (header.Version != GFF_VERSION3_2 && header.Version != GFF_VERSION3_3)
                throw new FileFormatException("BAD MAGIC");
            Type = (DataType)header.Type;
            r.Position(header.StructOffset);
            var headerStructs = r.ReadTArray<GFF_Struct>(sizeof(GFF_Struct), (int)header.StructCount);
            var structs = new IDictionary<string, object>[header.StructCount];
            for (var i = 0; i < structs.Length; i++)
            {

                structs[i] = new Dictionary<string, object>();
                if (headerStructs[i].Type == 0)
                    continue;
                structs[i].Add("_", headerStructs[i].Type);
            }
            r.Position(header.FieldOffset);
            var headerFields = r.ReadTArray<GFF_Field>(sizeof(GFF_Field), (int)header.FieldCount).Select<GFF_Field, (uint label, object value)>(x =>
            {
                switch (x.Type)
                {
                    case 0: return (x.LabelIndex, (byte)x.DataOrDataOffset);    //: Byte
                    case 1: return (x.LabelIndex, (char)x.DataOrDataOffset);    //: Char
                    case 2: return (x.LabelIndex, (ushort)x.DataOrDataOffset);  //: Word
                    case 3: return (x.LabelIndex, (short)x.DataOrDataOffset);   //: Short
                    case 4: return (x.LabelIndex, x.DataOrDataOffset);          //: DWord
                    case 5: return (x.LabelIndex, (int)x.DataOrDataOffset);     //: Int
                    case 8: return (x.LabelIndex, BitConverter.ToSingle(BitConverter.GetBytes(x.DataOrDataOffset), 0)); //: Float
                    case 14: return (x.LabelIndex, structs[x.DataOrDataOffset]); //: Struct
                    case 15: //: List
                        r.Position(header.ListIndicesOffset + x.DataOrDataOffset);
                        var list = new IDictionary<string, object>[(int)r.ReadUInt32()];
                        for (var i = 0; i < list.Length; i++)
                        {
                            var idx = r.ReadUInt32();
                            if (idx >= structs.Length)
                                throw new Exception();
                            list[i] = structs[idx];
                        }
                        return (x.LabelIndex, list);
                }
                r.Position(header.FieldDataOffset + x.DataOrDataOffset);
                switch (x.Type)
                {
                    case 6: return (x.LabelIndex, r.ReadUInt64());              //: DWord64
                    case 7: return (x.LabelIndex, r.ReadInt64());               //: Int64
                    case 9: return (x.LabelIndex, r.ReadDouble());              //: Double
                    case 10: return (x.LabelIndex, r.ReadL32ASCII());           //: CExoString
                    case 11: return (x.LabelIndex, new ResourceRef { Name = r.ReadL8ASCII() }); //: ResRef
                    case 12: //: CExoLocString
                        r.Skip(4);
                        var dialogID = r.ReadUInt32();
                        var values = new (uint id, string value)[r.ReadUInt32()];
                        for (var i = 0; i < values.Length; i++)
                            values[i] = (r.ReadUInt32(), r.ReadL32ASCII());
                        return (x.LabelIndex, new LocalizedRef { DialogID = dialogID, Values = values });
                    case 13: return (x.LabelIndex, r.ReadBytes((int)r.ReadUInt32()));
                }
                throw new ArgumentOutOfRangeException(nameof(x.Type), x.Type.ToString());
            }).ToArray();
            r.Position(header.LabelOffset);
            var headerLabels = r.ReadTArray<GFF_Label>(sizeof(GFF_Label), (int)header.LabelCount).Select(x => UnsafeUtils.ReadZASCII(x.Name, 0x10)).ToArray();
            // combine
            for (var i = 0; i < structs.Length; i++)
            {
                var fieldCount = headerStructs[i].FieldCount;
                var dataOrDataOffset = headerStructs[i].DataOrDataOffset;
                if (fieldCount == 1)
                {
                    var (label, value) = headerFields[dataOrDataOffset];
                    structs[i].Add(headerLabels[label], value);
                    continue;
                }
                var fields = structs[i];
                r.Position(header.FieldIndicesOffset + dataOrDataOffset);
                foreach (var idx in r.ReadTArray<uint>(sizeof(uint), (int)fieldCount))
                {
                    var (label, value) = headerFields[idx];
                    fields.Add(headerLabels[label], value);
                }
            }
            Root = structs[0];
        }
    }
}
