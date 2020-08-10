using GameEstate.Core;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameEstate.Formats.Valve
{
    public class ToolsAssetInfo
    {
        public const uint MAGIC = 0xC4CCACE8;
        public const uint GUARD = 0x049A48B2;

        public ToolsAssetInfo() { }
        public ToolsAssetInfo(BinaryReader r) => Read(r);

        public List<string> Mods { get; } = new List<string>();
        public List<string> Directories { get; } = new List<string>();
        public List<string> Filenames { get; } = new List<string>();
        public List<string> Extensions { get; } = new List<string>();
        public List<string> EditInfoKeys { get; } = new List<string>();
        public List<string> MiscStrings { get; } = new List<string>();
        public List<string> ConstructedFilepaths { get; } = new List<string>();

        public void Read(BinaryReader r)
        {
            if (r.ReadUInt32() != MAGIC)
                throw new InvalidDataException("Given file is not tools_asset_info.");

            var version = r.ReadUInt32();
            if (version != 10)
                throw new InvalidDataException($"Unsupported version: {version}");
            var fileCount = r.ReadUInt32();
            var b = r.ReadUInt32(); // block id?
            if (b != 1)
                throw new InvalidDataException($"b is {b}");

            ReadStringsBlock(r, Mods);
            ReadStringsBlock(r, Directories);
            ReadStringsBlock(r, Filenames);
            ReadStringsBlock(r, Extensions);
            ReadStringsBlock(r, EditInfoKeys);
            ReadStringsBlock(r, MiscStrings);

            for (var i = 0; i < fileCount; i++)
            {
                var hash = r.ReadUInt64();
                var unk1 = (int)(hash >> 61) & 7;
                var addonIndex = (int)(hash >> 52) & 0x1FF;
                var directoryIndex = (int)(hash >> 33) & 0x7FFFF;
                var filenameIndex = (int)(hash >> 10) & 0x7FFFFF;
                var extensionIndex = (int)(hash & 0x3FF);
                //Console.WriteLine($"{unk1} {addonIndex} {directoryIndex} {filenameIndex} {extensionIndex}");
                var path = new StringBuilder();
                if (addonIndex != 0x1FF)
                {
                    path.Append(Mods[addonIndex]);
                    path.Append("/");
                }
                if (directoryIndex != 0x7FFFF)
                {
                    path.Append(Directories[directoryIndex]);
                    path.Append("/");
                }
                if (filenameIndex != 0x7FFFFF)
                {
                    path.Append(Filenames[filenameIndex]);
                }
                if (extensionIndex != 0x3FF)
                {
                    path.Append(".");
                    path.Append(Extensions[extensionIndex]);
                }
                ConstructedFilepaths.Add(path.ToString());
            }
        }

        static void ReadStringsBlock(BinaryReader r, ICollection<string> output)
        {
            var count = r.ReadUInt32();
            for (var i = 0U; i < count; i++)
                output.Add(r.ReadZUTF8());
        }

        public override string ToString()
        {
            var b = new StringBuilder();
            foreach (var str in ConstructedFilepaths)
                b.AppendLine(str);
            return b.ToString();
        }
    }
}
