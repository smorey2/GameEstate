using System;
using System.IO;
using System.Collections.Generic;
using GameEstate.Core;

namespace GameEstate.Formats.AC.Entity
{
    public class StringTableData
    {
        public readonly uint Id;
        public readonly string[] VarNames;
        public readonly string[] Vars;
        public readonly string[] Strings;
        public readonly uint[] Comments;
        public readonly byte Unknown;

        public StringTableData(BinaryReader r)
        {
            Id = r.ReadUInt32();
            VarNames = r.ReadL16Array(x => x.ReadUnicodeString());
            Vars = r.ReadL16Array(x => x.ReadUnicodeString());
            Strings = r.ReadL32Array(x => x.ReadUnicodeString());
            Comments = r.ReadL32Array<uint>(sizeof(uint));
            Unknown = r.ReadByte();
        }
    }
}
