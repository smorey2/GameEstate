﻿using GameEstate.Core;
using GameEstate.Data;
using System;
using System.IO;
using System.Linq;
using static GameEstate.EstateDebug;

namespace GameEstate.Formats.Tes
{
    public class FieldHeader
    {
        public override string ToString() => Type;
        public string Type; // 4 bytes
        public int DataSize;

        public FieldHeader(BinaryReader r, TesFormat format)
        {
            Type = r.ReadASCII(4);
            DataSize = (int)(format == TesFormat.TES3 ? r.ReadUInt32() : r.ReadUInt16());
        }
    }

    public abstract class Record : IRecord
    {
        internal Header Header;
        public uint Id => Header.FormId;

        /// <summary>
        /// Return an uninitialized subrecord to deserialize, or null to skip.
        /// </summary>
        /// <returns>Return an uninitialized subrecord to deserialize, or null to skip.</returns>
        public abstract bool CreateField(BinaryReader r, TesFormat format, string type, int dataSize);

        public void Read(BinaryReader r, string filePath, TesFormat format)
        {
            var startPosition = r.Position();
            var endPosition = startPosition + Header.DataSize;
            while (r.BaseStream.Position < endPosition)
            {
                var fieldHeader = new FieldHeader(r, format);
                if (fieldHeader.Type == "XXXX")
                {
                    if (fieldHeader.DataSize != 4)
                        throw new InvalidOperationException();
                    fieldHeader.DataSize = (int)r.ReadUInt32();
                    continue;
                }
                else if (fieldHeader.Type == "OFST" && Header.Type == "WRLD")
                {
                    r.Position(endPosition);
                    continue;
                }
                var position = r.BaseStream.Position;
                if (!CreateField(r, format, fieldHeader.Type, fieldHeader.DataSize))
                {
                    Log($"Unsupported ESM record type: {Header.Type}:{fieldHeader.Type}");
                    r.Skip(fieldHeader.DataSize);
                    continue;
                }
                // check full read
                if (r.BaseStream.Position != position + fieldHeader.DataSize)
                    throw new FormatException($"Failed reading {Header.Type}:{fieldHeader.Type} field data at offset {position} in {filePath} of {r.BaseStream.Position - position - fieldHeader.DataSize}");
            }
            // check full read
            if (r.Position() != endPosition)
                throw new FormatException($"Failed reading {Header.Type} record data at offset {startPosition} in {filePath}");
        }
    }
}
