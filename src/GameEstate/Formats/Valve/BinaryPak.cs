using GameEstate.Core;
using GameEstate.Formats.Valve.Blocks;
using GameEstate.Formats.Valve.Shader;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameEstate.Formats.Valve
{
    public class BinaryPak
    {
        const ushort KnownHeaderVersion = 12;

        public uint FileSize { get; private set; }

        public ushort Version { get; private set; }

        public RERL RERL => GetBlockByType<RERL>();
        public REDI REDI => GetBlockByType<REDI>();
        public NTRO NTRO => GetBlockByType<NTRO>();
        public VBIB VBIB => GetBlockByType<VBIB>();
        public DATA DATA => GetBlockByType<DATA>();

        public T GetBlockByIndex<T>(int index) where T : Block => Blocks[index] as T;

        public T GetBlockByType<T>() where T : Block => (T)Blocks.Find(b => b.GetType() == typeof(T));

        public bool ContainsBlockType<T>() where T : Block => Blocks.Exists(b => b.GetType() == typeof(T));

        public bool TryGetBlockType<T>(out T value) where T : Block => (value = (T)Blocks.Find(b => b.GetType() == typeof(T))) != null;

        public List<Block> Blocks { get; }

        public DATA.DataType DataType { get; set; }

        public void Read(BinaryReader r)
        {
            FileSize = r.ReadUInt32();
            if (FileSize == 0x55AA1234)
                throw new InvalidDataException("VPK file");
            if (FileSize == CompiledShader.MAGIC)
                throw new InvalidDataException("Shader file");
            if (FileSize != r.BaseStream.Length) { }
            var headerVersion = r.ReadUInt16();
            if (headerVersion != KnownHeaderVersion)
                throw new InvalidDataException($"Bad header version. ({headerVersion} != expected {KnownHeaderVersion})");
            Version = r.ReadUInt16();
            var blockOffset = r.ReadUInt32();
            var blockCount = r.ReadUInt32();
            r.Skip(blockOffset - 8); // 8 is 2 uint32s we just read
            for (var i = 0; i < blockCount; i++)
            {
                var blockType = Encoding.UTF8.GetString(r.ReadBytes(4));
                var position = r.BaseStream.Position;
                var offset = (uint)position + r.ReadUInt32();
                var size = r.ReadUInt32();
                Block block = null;
                // Peek data to detect VKV3
                if (size >= 4 && blockType == "DATA" && !DATA.IsHandledType(DataType))
                    r.Peek(0, () =>
                    {
                        var magic = r.ReadUInt32();
                        if (magic == DATABinaryKV3.MAGIC || magic == DATABinaryKV3.MAGIC2)
                            block = new DATABinaryKV3();
                    });
                if (block == null)
                    block = Block.Factory(this, blockType);
                block.Offset = offset;
                block.Size = size;
                if (blockType == "REDI" || blockType == "NTRO")
                    block.Read(r, this);
                Blocks.Add(block);
                switch (block)
                {
                    case REDI redi:
                        // Try to determine resource type by looking at first compiler indentifier
                        if (DataType == DATA.DataType.Unknown && REDI.Structs.ContainsKey(REDI.REDIStruct.SpecialDependencies))
                        {
                            var specialDeps = (REDISpecialDependencies)REDI.Structs[REDI.REDIStruct.SpecialDependencies];
                            if (specialDeps.List.Count > 0)
                                DataType = DATA.DetermineTypeByCompilerIdentifier(specialDeps.List[0]);
                        }
                        break;
                    case NTRO ntro:
                        if (DataType == DATA.DataType.Unknown && NTRO.ReferencedStructs.Count > 0)
                            switch (NTRO.ReferencedStructs[0].Name)
                            {
                                case "VSoundEventScript_t": DataType = DATA.DataType.SoundEventScript; break;
                                case "CWorldVisibility": DataType = DATA.DataType.WorldVisibility; break;
                            }
                        break;
                }
                r.BaseStream.Position = position + 8;
            }
            foreach (var block in Blocks)
                if (!(block is REDI) && !(block is NTRO))
                    block.Read(r, this);
        }
    }
}
