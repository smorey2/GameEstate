using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.Valve.Blocks;
using GameEstate.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameEstate.Formats.Valve
{
    public class BinaryPak : IDisposable, IGetExplorerInfo, IRedirected<ITextureInfo>, IRedirected<IMaterialInfo>, IRedirected<IMeshInfo>, IRedirected<IModelInfo>
    {
        public const ushort KnownHeaderVersion = 12;

        public BinaryPak() { }
        public BinaryPak(BinaryReader r) => Read(r);

        public void Dispose()
        {
            Reader?.Dispose();
            Reader = null;
        }

        ITextureInfo IRedirected<ITextureInfo>.Value => DATA as ITextureInfo;
        IMaterialInfo IRedirected<IMaterialInfo>.Value => DATA as IMaterialInfo;
        IMeshInfo IRedirected<IMeshInfo>.Value => DataType == DATA.DataType.Mesh ? new DATAMesh(this) as IMeshInfo : null;
        IModelInfo IRedirected<IModelInfo>.Value => DATA as IModelInfo;

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode("BinaryPak", items: new List<ExplorerInfoNode> {
                    new ExplorerInfoNode($"FileSize: {FileSize}"),
                    new ExplorerInfoNode($"Version: {Version}"),
                    new ExplorerInfoNode($"Blocks: {Blocks.Count}"),
                    new ExplorerInfoNode($"DataType: {DataType}"),
                })
            };
            switch (DataType)
            {
                case DATA.DataType.Texture:
                    {
                        var data = (DATATexture)DATA;
                        try
                        {
                            nodes.AddRange(new List<ExplorerInfoNode> {
                                //new ExplorerInfoNode(null, new ExplorerContentTab { Type = "Text", Name = Path.GetFileName(file.Path), Value = "PICTURE" }), //(tex.GenerateBitmap().ToBitmap(), tex.Width, tex.Height)
                                new ExplorerInfoNode(null, new ExplorerContentTab { Type = "Texture", Name = "Texture", Value = this, Dispose = this }),
                                new ExplorerInfoNode("Texture", items: new List<ExplorerInfoNode> {
                                    new ExplorerInfoNode($"Width: {data.Width}"),
                                    new ExplorerInfoNode($"Height: {data.Height}"),
                                    new ExplorerInfoNode($"NumMipMaps: {data.NumMipMaps}"),
                                })
                            });
                        }
                        catch (Exception e)
                        {
                            nodes.Add(new ExplorerInfoNode(null, new ExplorerContentTab { Type = "Text", Name = "Exception", Value = e.Message }));
                        }
                    }
                    break;
                case DATA.DataType.Panorama:
                    {
                        var data = (DATAPanorama)DATA;
                        nodes.AddRange(new List<ExplorerInfoNode> {
                            new ExplorerInfoNode(null, new ExplorerContentTab { Type = "DataGrid", Name = "Panorama Names", Value = data.Names }),
                            new ExplorerInfoNode("Panorama", items: new List<ExplorerInfoNode> {
                                new ExplorerInfoNode($"Names: {data.Names.Count}"),
                            })
                        });
                    }
                    break;
                case DATA.DataType.PanoramaLayout:
                    break;
                case DATA.DataType.PanoramaScript:
                    break;
                case DATA.DataType.PanoramaStyle:
                    break;
                case DATA.DataType.Particle:
                    nodes.Add(new ExplorerInfoNode(null, new ExplorerContentTab { Type = "Particle", Name = "Particle", Value = (DATAParticleSystem)DATA, Dispose = this }));
                    break;
                case DATA.DataType.Sound:
                    {
                        var sound = (DATASound)DATA;
                        var stream = sound.GetSoundStream();
                        nodes.Add(new ExplorerInfoNode(null, new ExplorerContentTab { Type = "AudioPlayer", Name = "Sound", Value = stream, Tag = sound.SoundType.ToString(), Dispose = this }));
                    }
                    break;
                case DATA.DataType.World:
                    nodes.Add(new ExplorerInfoNode(null, new ExplorerContentTab { Type = "World", Name = "World", Value = (DATAWorld)DATA, Dispose = this }));
                    break;
                case DATA.DataType.WorldNode:
                    nodes.Add(new ExplorerInfoNode(null, new ExplorerContentTab { Type = "World", Name = "World Node", Value = (DATAWorldNode)DATA, Dispose = this }));
                    break;
                case DATA.DataType.Model:
                    nodes.Add(new ExplorerInfoNode(null, new ExplorerContentTab { Type = "Model", Name = "Model", Value = this, Dispose = this }));
                    break;
                case DATA.DataType.Mesh:
                    nodes.Add(new ExplorerInfoNode(null, new ExplorerContentTab { Type = "Model", Name = "Mesh", Value = this, Dispose = this }));
                    break;
                case DATA.DataType.Material:
                    nodes.Add(new ExplorerInfoNode(null, new ExplorerContentTab { Type = "Material", Name = "Material", Value = this, Dispose = this }));
                    break;
            }
            foreach (var block in Blocks)
            {
                if (block is RERL repl)
                {
                    nodes.Add(new ExplorerInfoNode(null, new ExplorerContentTab { Type = "DataGrid", Name = "External Refs", Value = repl.RERLInfos }));
                    continue;
                }
                else if (block is NTRO ntro)
                {
                    if (ntro.ReferencedStructs.Count > 0)
                        nodes.Add(new ExplorerInfoNode(null, new ExplorerContentTab { Type = "DataGrid", Name = "Introspection Manifest: Structs", Value = ntro.ReferencedStructs }));
                    if (ntro.ReferencedEnums.Count > 0)
                        nodes.Add(new ExplorerInfoNode(null, new ExplorerContentTab { Type = "DataGrid", Name = "Introspection Manifest: Enums", Value = ntro.ReferencedEnums }));
                }
                var tag = new ExplorerContentTab { Type = "Text", Name = block.GetType().Name };
                nodes.Add(new ExplorerInfoNode(null, tag));
                if (block is DATA)
                    switch (DataType)
                    {
                        case DATA.DataType.Sound: tag.Value = ((DATASound)block).ToString(); break;
                        case DATA.DataType.Particle:
                        case DATA.DataType.Mesh:
                            if (block is DATABinaryKV3 kv3) tag.Value = kv3.ToString();
                            else if (block is NTRO blockNTRO) tag.Value = blockNTRO.ToString();
                            break;
                        default: tag.Value = block.ToString(); break;
                    }
                else tag.Value = block.ToString();
            }
            if (!nodes.Any(x => (x.Tag as ExplorerContentTab)?.Dispose != null))
                Dispose();
            return nodes;
        }

        public BinaryReader Reader { get; private set; }

        public uint FileSize { get; private set; }

        public ushort Version { get; private set; }

        public RERL RERL => GetBlockByType<RERL>();
        public REDI REDI => GetBlockByType<REDI>();
        public NTRO NTRO => GetBlockByType<NTRO>();
        public VBIB VBIB => GetBlockByType<VBIB>();
        public DATA DATA => GetBlockByType<DATA>();

        public T GetBlockByIndex<T>(int index) where T : Block => Blocks[index] as T;

        public T GetBlockByType<T>() where T : Block => (T)Blocks.Find(b => typeof(T).IsAssignableFrom(b.GetType()));

        public bool ContainsBlockType<T>() where T : Block => Blocks.Exists(b => typeof(T).IsAssignableFrom(b.GetType()));

        public bool TryGetBlockType<T>(out T value) where T : Block => (value = (T)Blocks.Find(b => typeof(T).IsAssignableFrom(b.GetType()))) != null;

        public List<Block> Blocks { get; } = new List<Block>();

        public DATA.DataType DataType { get; set; }

        public void Read(BinaryReader r)
        {
            Reader = r;
            FileSize = r.ReadUInt32();
            if (FileSize == 0x55AA1234)
                throw new InvalidDataException("VPK file");
            if (FileSize == CompiledShader.MAGIC)
                throw new InvalidDataException("Shader file");
            if (FileSize != r.BaseStream.Length) { }
            var headerVersion = r.ReadUInt16();
            if (headerVersion != KnownHeaderVersion)
                throw new FileFormatException($"Bad Magic: {headerVersion}, expected {KnownHeaderVersion}");
            Version = r.ReadUInt16();
            var blockOffset = r.ReadUInt32();
            var blockCount = r.ReadUInt32();
            r.Skip(blockOffset - 8); // 8 is uint32 x2 we just read
            for (var i = 0; i < blockCount; i++)
            {
                var blockType = Encoding.UTF8.GetString(r.ReadBytes(4));
                var startPosition = r.BaseStream.Position;
                var offset = (uint)startPosition + r.ReadUInt32();
                var size = r.ReadUInt32();
                var block = (size >= 4 && blockType == "DATA" && !DATA.IsHandledType(DataType)
                    ? r.Peek(z => { var magic = z.ReadUInt32(); return magic == DATABinaryKV3.MAGIC || magic == DATABinaryKV3.MAGIC2 ? (Block)new DATABinaryKV3() : null; })
                    : null)
                    ?? Block.Factory(this, blockType);
                block.Offset = offset;
                block.Size = size;
                if (blockType == "REDI" || blockType == "NTRO")
                    block.Read(this, r);
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
                r.BaseStream.Position = startPosition + 8;
            }
            foreach (var block in Blocks)
                if (!(block is REDI) && !(block is NTRO))
                    block.Read(this, r);
        }
    }
}
