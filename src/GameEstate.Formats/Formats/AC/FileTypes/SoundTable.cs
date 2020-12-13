using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Entity;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.FileTypes
{
    /// <summary>
    /// SoundTable files contain a listing of which Wav types to play in response to certain events.
    /// They are located in the client_portal.dat and are files starting with 0x20
    /// </summary>
    [PakFileType(PakFileType.SoundTable)]
    public class SoundTable : AbstractFileType, IGetExplorerInfo
    {
        public readonly uint Unknown; // As the name implies, not sure what this is
        // Not quite sure what this is for, but it's the same in every file.
        public readonly SoundTableData[] SoundHash;
        // The uint key corresponds to an Enum.Sound
        public readonly Dictionary<uint, SoundData> Data;

        public SoundTable(BinaryReader r)
        {
            Id = r.ReadUInt32();
            Unknown = r.ReadUInt32();
            SoundHash = r.ReadL32Array(x => new SoundTableData(x));
            Data = r.ReadL16Many<uint, SoundData>(sizeof(uint), x => new SoundData(x), offset: 2);
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(SoundTable)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                    //new ExplorerInfoNode($"Type: {Type}"),
                })
            };
            return nodes;
        }
    }
}
