using ACE.DatLoader.Entity;
using System;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.FileTypes
{
    [PakFileType(PakFileType.ChatPoseTable)]
    public class ChatPoseTable : FileType
    {
        public const uint FILE_ID = 0x0E000007;

        // Key is a emote command, value is the state you are enter into
        public Dictionary<string, string> ChatPoseHash = new Dictionary<string, string>();

        // Key is the state, value are the strings that players see during the emote
        public Dictionary<string, ChatEmoteData> ChatEmoteHash = new Dictionary<string, ChatEmoteData>();

        public override void Read(BinaryReader reader)
        {
            Id = reader.ReadUInt32();

            var totalObjects = reader.ReadUInt16();
            reader.ReadUInt16(); // var bucketSize
            for (int i = 0; i < totalObjects; i++)
            {
                string key = reader.ReadL16String(Encoding.Default); reader.AlignBoundary();
                string value = reader.ReadL16String(Encoding.Default); reader.AlignBoundary();
                ChatPoseHash.Add(key, value);
            }

            var totalEmoteObjects = reader.ReadUInt16();
            reader.ReadUInt16();// var bucketSize
            for (int i = 0; i < totalEmoteObjects; i++)
            {
                string key = reader.ReadL16String(Encoding.Default); reader.AlignBoundary();
                ChatEmoteData value = new ChatEmoteData();
                value.Unpack(reader);
                ChatEmoteHash.Add(key, value);
            }
        }
    }
}
