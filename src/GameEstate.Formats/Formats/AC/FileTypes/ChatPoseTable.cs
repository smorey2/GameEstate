using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Entity;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameEstate.Formats.AC.FileTypes
{
    [PakFileType(PakFileType.ChatPoseTable)]
    public class ChatPoseTable : AbstractFileType, IGetExplorerInfo
    {
        public const uint FILE_ID = 0x0E000007;

        // Key is a emote command, value is the state you are enter into
        public readonly Dictionary<string, string> ChatPoseHash;
        // Key is the state, value are the strings that players see during the emote
        public readonly Dictionary<string, ChatEmoteData> ChatEmoteHash;

        public ChatPoseTable(BinaryReader r)
        {
            Id = r.ReadUInt32();
            ChatPoseHash = r.ReadL16Many(x => { var v = x.ReadL16ANSI(Encoding.Default); x.AlignBoundary(); return v; }, x => { var v = x.ReadL16ANSI(Encoding.Default); x.AlignBoundary(); return v; }, offset: 2);
            ChatEmoteHash = r.ReadL16Many(x => { var v = x.ReadL16ANSI(Encoding.Default); x.AlignBoundary(); return v; }, x => new ChatEmoteData(x), offset: 2);
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(ChatPoseTable)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                })
            };
            return nodes;
        }
    }
}
