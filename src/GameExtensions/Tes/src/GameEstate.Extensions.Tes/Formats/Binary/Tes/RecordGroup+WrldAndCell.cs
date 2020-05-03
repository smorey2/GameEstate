using GameEstate.Formats.Binary.Tes.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameEstate.Formats.Binary.Tes
{
    partial class RecordGroup
    {
        internal HashSet<byte[]> _ensureCELLsByLabel;
        internal Dictionary<Vector3Int, CELLRecord> CELLsById;
        internal Dictionary<Vector3Int, LANDRecord> LANDsById;

        public RecordGroup[] EnsureWrldAndCell(Vector3Int cellId)
        {
            var cellBlockX = (short)(cellId.x >> 5);
            var cellBlockY = (short)(cellId.y >> 5);
            var cellBlockId = new byte[4];
            Buffer.BlockCopy(BitConverter.GetBytes(cellBlockY), 0, cellBlockId, 0, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(cellBlockX), 0, cellBlockId, 2, 2);
            Load();
            if (GroupsByLabel.TryGetValue(cellBlockId, out var cellBlocks))
                return cellBlocks.Select(x => x.EnsureCell(cellId)).ToArray();
            return null;
        }

        //  = nxn[nbits] + 4x4[2bits] + 8x8[3bit]
        public RecordGroup EnsureCell(Vector3Int cellId)
        {
            if (_ensureCELLsByLabel == null)
                _ensureCELLsByLabel = new HashSet<byte[]>(ByteArrayComparer.Default);
            var cellBlockX = (short)(cellId.x >> 5);
            var cellBlockY = (short)(cellId.y >> 5);
            var cellSubBlockX = (short)(cellId.x >> 3);
            var cellSubBlockY = (short)(cellId.y >> 3);
            var cellSubBlockId = new byte[4];
            Buffer.BlockCopy(BitConverter.GetBytes(cellSubBlockY), 0, cellSubBlockId, 0, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(cellSubBlockX), 0, cellSubBlockId, 2, 2);
            if (_ensureCELLsByLabel.Contains(cellSubBlockId))
                return this;
            Load();
            if (CELLsById == null)
                CELLsById = new Dictionary<Vector3Int, CELLRecord>();
            if (LANDsById == null && cellId.z >= 0)
                LANDsById = new Dictionary<Vector3Int, LANDRecord>();
            if (GroupsByLabel.TryGetValue(cellSubBlockId, out var cellSubBlocks))
            {
                // find cell
                var cellSubBlock = cellSubBlocks.Single();
                cellSubBlock.Load(true);
                foreach (var cell in cellSubBlock.Records.Cast<CELLRecord>())
                {
                    cell.GridId = new Vector3Int(cell.XCLC.Value.GridX, cell.XCLC.Value.GridY, !cell.IsInterior ? cellId.z : -1);
                    CELLsById.Add(cell.GridId, cell);
                    // find children
                    if (cellSubBlock.GroupsByLabel.TryGetValue(BitConverter.GetBytes(cell.Id), out var cellChildren))
                    {
                        var cellChild = cellChildren.Single();
                        var cellTemporaryChildren = cellChild.Groups.Single(x => x.Headers.First().GroupType == Header.HeaderGroupType.CellTemporaryChildren);
                        foreach (var land in cellTemporaryChildren.Records.Cast<LANDRecord>())
                        {
                            land.GridId = new Vector3Int(cell.XCLC.Value.GridX, cell.XCLC.Value.GridY, !cell.IsInterior ? cellId.z : -1);
                            LANDsById.Add(land.GridId, land);
                        }
                    }
                }
                _ensureCELLsByLabel.Add(cellSubBlockId);
                return this;
            }
            return null;
        }
    }
}
