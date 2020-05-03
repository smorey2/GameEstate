﻿using GameEstate.Core;
using GameEstate.Data;
using GameEstate.Formats.Binary.Tes;
using GameEstate.Formats.Binary.Tes.Records;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// TES3
//http://en.uesp.net/wiki/Tes3Mod:File_Format
//https://github.com/TES5Edit/TES5Edit/blob/dev/wbDefinitionsTES3.pas
//http://en.uesp.net/morrow/tech/mw_esm.txt
//https://github.com/mlox/mlox/blob/master/util/tes3cmd/tes3cmd
// TES4
//https://github.com/WrinklyNinja/esplugin/tree/master/src
//http://en.uesp.net/wiki/Tes4Mod:Mod_File_Format
//https://github.com/TES5Edit/TES5Edit/blob/dev/wbDefinitionsTES4.pas 
// TES5
//http://en.uesp.net/wiki/Tes5Mod:Mod_File_Format
//https://github.com/TES5Edit/TES5Edit/blob/dev/wbDefinitionsTES5.pas 

namespace GameEstate.Formats.Binary
{
    /// <summary>
    /// DatFormatTes
    /// </summary>
    /// <seealso cref="GameEstate.Formats.Binary.DatFormat" />
    public class DatFormatTes : DatFormat
    {
        const int RecordHeaderSizeInBytes = 16;
        public TesFormat Format;
        public Dictionary<string, RecordGroup> Groups;

        static TesFormat GetFormat(string game)
        {
            switch (game)
            {
                // tes
                case "Morrowind": return TesFormat.TES3;
                case "Oblivion": return TesFormat.TES4;
                case "Skyrim":
                case "SkyrimSE":
                case "SkyrimVR": return TesFormat.TES5;
                // fallout
                case "Fallout3":
                case "FalloutNV": return TesFormat.TES4;
                case "Fallout4":
                case "Fallout4VR": return TesFormat.TES5;
                default: throw new ArgumentOutOfRangeException(nameof(game), game);
            }
        }

        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="r">The r.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">stage</exception>
        public override Task ReadAsync(CoreDatFile source, BinaryReader r, ReadStage stage)
        {
            if (stage != ReadStage.File)
                throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());
            Format = GetFormat(source.Game);
            Read(source, r, 1);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Reads the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="r">The r.</param>
        /// <param name="recordLevel">The record level.</param>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        void Read(CoreDatFile source, BinaryReader r, int recordLevel)
        {
            var filePath = source.FilePath;
            var poolAction = (GenericPoolAction<BinaryReader>)source.Pool.Action;
            var rootHeader = new Header(r, Format, null);
            if ((Format == TesFormat.TES3 && rootHeader.Type != "TES3") || (Format != TesFormat.TES3 && rootHeader.Type != "TES4"))
                throw new FormatException($"{filePath} record header {rootHeader.Type} is not valid for this {Format}");
            var rootRecord = rootHeader.CreateRecord(rootHeader.Position, recordLevel);
            rootRecord.Read(r, filePath, Format);
            // morrowind hack
            if (Format == TesFormat.TES3)
            {
                var group = new RecordGroup(poolAction, filePath, Format, recordLevel);
                group.AddHeader(new Header
                {
                    Label = null,
                    DataSize = (uint)(r.BaseStream.Length - r.Position()),
                    Position = r.Position(),
                });
                group.Load();
                Groups = group.Records.GroupBy(x => x.Header.Type)
                    .ToDictionary(x => x.Key, x =>
                    {
                        var s = new RecordGroup(null, filePath, Format, recordLevel) { Records = x.ToList() };
                        s.AddHeader(new Header { Label = Encoding.ASCII.GetBytes(x.Key) }, load: false);
                        return s;
                    });
                return;
            }
            // read groups
            Groups = new Dictionary<string, RecordGroup>();
            var endPosition = r.BaseStream.Length;
            while (r.BaseStream.Position < endPosition)
            {
                var header = new Header(r, Format, null);
                if (header.Type != "GRUP")
                    throw new InvalidOperationException($"{header.Type} not GRUP");
                var nextPosition = r.Position() + header.DataSize;
                var label = Encoding.ASCII.GetString(header.Label);
                if (!Groups.TryGetValue(label, out var group))
                {
                    group = new RecordGroup(poolAction, filePath, Format, recordLevel);
                    Groups.Add(label, group);
                }
                group.AddHeader(header);
                r.Position(nextPosition);
            }
        }

        /// <summary>
        /// Writes the asynchronous.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="w">The w.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Task WriteAsync(CoreDatFile source, BinaryWriter w, WriteStage stage) => throw new NotImplementedException();

        // TES3
        Dictionary<string, IRecord> MANYsById;
        Dictionary<long, LTEXRecord> LTEXsById;
        Dictionary<Vector3Int, LANDRecord> LANDsById;
        Dictionary<Vector3Int, CELLRecord> CELLsById;
        Dictionary<string, CELLRecord> CELLsByName;

        // TES4
        Dictionary<uint, Tuple<WRLDRecord, RecordGroup[]>> WRLDsById;
        Dictionary<string, LTEXRecord> LTEXsByEid;

        public override void Process(CoreDatFile source)
        {
            if (Format == TesFormat.TES3)
            {
                var statGroups = new List<Record>[] { Groups.ContainsKey("STAT") ? Groups["STAT"].Load() : null };
                MANYsById = statGroups.SelectMany(x => x).Cast<IHaveEDID>().Where(x => x != null).ToDictionary(x => x.EDID.Value, x => (IRecord)x);
                LTEXsById = Groups["LTEX"].Load().Cast<LTEXRecord>().ToDictionary(x => x.INTV.Value);
                var lands = Groups["LAND"].Load().Cast<LANDRecord>().ToList();
                foreach (var land in lands)
                    land.GridId = new Vector3Int(land.INTV.CellX, land.INTV.CellY, 0);
                LANDsById = lands.ToDictionary(x => x.GridId);
                var cells = Groups["CELL"].Load().Cast<CELLRecord>().ToList();
                foreach (var cell in cells)
                    cell.GridId = new Vector3Int(cell.XCLC.Value.GridX, cell.XCLC.Value.GridY, !cell.IsInterior ? 0 : -1);
                CELLsById = cells.Where(x => !x.IsInterior).ToDictionary(x => x.GridId);
                CELLsByName = cells.Where(x => x.IsInterior).ToDictionary(x => x.EDID.Value);
                return;
            }
            var wrldsByLabel = Groups["WRLD"].GroupsByLabel;
            WRLDsById = Groups["WRLD"].Load().Cast<WRLDRecord>().ToDictionary(x => x.Id, x =>
            {
                wrldsByLabel.TryGetValue(BitConverter.GetBytes(x.Id), out var wrlds);
                return new Tuple<WRLDRecord, RecordGroup[]>(x, wrlds);
            });
            LTEXsByEid = Groups["LTEX"].Load().Cast<LTEXRecord>().ToDictionary(x => x.EDID.Value);
        }
    }
}