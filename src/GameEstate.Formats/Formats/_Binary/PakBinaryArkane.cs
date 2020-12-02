﻿using GameEstate.Core;
using GameEstate.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class PakBinaryArkane : PakBinary
    {
        public static readonly PakBinary Instance = new PakBinaryArkane();
        PakBinaryArkane() { }

        const uint RES_MAGIC = 0x04534552;

        class SubPakFile : BinaryPakManyFile
        {
            public SubPakFile(Estate estate, string game, string filePath, object tag = null) : base(estate, game, filePath, Instance, tag)
            {
                Open();
            }
        }

        // object factory
        static Func<BinaryReader, FileMetadata, Task<object>> ObjectFactory(string path)
        {
            Task<object> DdsFactory(BinaryReader r, FileMetadata f)
            {
                var tex = new TextureInfo();
                tex.ReadDds(r);
                return Task.FromResult((object)tex);
            }
            switch (Path.GetExtension(path).ToLowerInvariant())
            {
                case ".dds": return DdsFactory;
                default: return null;
            }
        }

        public unsafe override Task ReadAsync(BinaryPakFile source, BinaryReader r, ReadStage stage)
        {
            if (!(source is BinaryPakManyFile multiSource))
                throw new NotSupportedException();
            if (stage != ReadStage.File)
                throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());

            if (Path.GetExtension(source.FilePath) != ".index")
                throw new ArgumentOutOfRangeException("must be index");
            if (Path.GetFileName(source.FilePath) == "master.index")
            {
                const uint SubMarker = 0x18000000;
                const uint EndMarker = 0x01000000;

                var magic = Utility.Reverse(r.ReadUInt32());
                if (magic != RES_MAGIC)
                    throw new FileFormatException("BAD MAGIC");
                r.Skip(4);
                var files2 = multiSource.Files = new List<FileMetadata>();
                var state = 0;
                do
                {
                    var nameSize = r.ReadUInt32();
                    if (nameSize == SubMarker) { state++; nameSize = r.ReadUInt32(); }
                    else if (nameSize == EndMarker) break;
                    var path = r.ReadASCII((int)nameSize).Replace('\\', '/');
                    var packId = state > 0 ? r.ReadUInt16() : 0;
                    files2.Add(new FileMetadata
                    {
                        Path = path,
                        Pak = new SubPakFile(source.Estate, source.Game, path),
                    });
                }
                while (true);
                return Task.CompletedTask;
            }

            var pathFile = Path.GetFileName(source.FilePath);
            var pathDir = Path.GetDirectoryName(source.FilePath);
            var resourcePath = Path.Combine(pathDir, $"{pathFile.Substring(0, pathFile.Length - 6)}.resources");
            if (!File.Exists(resourcePath))
                throw new ArgumentOutOfRangeException("Unable to find resources extension");
            var sharedResourcePath = new[] {
                "shared_2_3.sharedrsc",
                "shared_2_3_4.sharedrsc",
                "shared_1_2_3.sharedrsc",
                "shared_1_2_3_4.sharedrsc" }
                .Select(x => Path.Combine(pathDir, x)).FirstOrDefault(File.Exists);
            if (sharedResourcePath == null)
                throw new ArgumentOutOfRangeException("Unable to find Sharedrsc");

            r.Position(4);
            var mainFileSize = Utility.Reverse(r.ReadUInt32()); // mainFileSize
            r.Skip(24);
            var numFiles = Utility.Reverse(r.ReadUInt32());
            var files = multiSource.Files = new FileMetadata[numFiles];
            for (var i = 0; i < numFiles; i++)
            {
                var id = Utility.Reverse(r.ReadUInt32());
                var tag1 = r.ReadL32ASCII();
                var tag2 = r.ReadL32ASCII();
                var path = r.ReadL32ASCII().Replace('\\', '/');
                var position = Utility.Reverse(r.ReadUInt64());
                var fileSize = Utility.Reverse(r.ReadUInt32());
                var packedSize = Utility.Reverse(r.ReadUInt32());
                r.Skip(4);
                var flags = Utility.Reverse(r.ReadUInt32());
                var flags2 = Utility.Reverse(r.ReadUInt16());
                var useSharedResources = (flags & 32) != 0 && flags2 == 0x8000;
                var newPath = !useSharedResources ? resourcePath : sharedResourcePath;
                files[i] = new FileMetadata
                {
                    Id = (int)id,
                    Path = path,
                    ObjectFactory = ObjectFactory(path),
                    Compressed = fileSize != packedSize ? 1 : 0,
                    FileSize = fileSize,
                    PackedSize = packedSize,
                    Position = (long)position,
                    Tag = (newPath, tag1, tag2),
                };
            }
            return Task.CompletedTask;
        }

        // Bad Positions - Dishonored2
        static HashSet<long> _badPositions = new HashSet<long> {
            293, //: generated/decls/renderparm/atm/worldfog/artistscatteringcolor.decl
            917004, //: generated/decls/renderparm/ocean/patchtransform.decl
            9923823, //: generated/decls/soundevent/sound_events/bsp/bsp_physmat/bsp_foosteps/bsp_fs_player/emily/fs_e_metal_chandelier/fs_e_metal_chandelier_w.decl
            9924002, //: generated/decls/fx/contactsystem/w.emily_env.metal.chandelier.fx.decl
            32872162, //: generated/image/models/effects/textures/gameplay/blood/blood_leads_05_fl.bimage7
            32966564, //: generated/decls/material/models/effects/materials/gameplay/blood/blood_leads_05_bf.material.decl
            45704814, //: generated/decls/fx/contactsystem/pr.ar.venom_env.tile.fx.decl
        };

        public override Task<Stream> ReadDataAsync(BinaryPakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            if (file.FileSize == 0 || _badPositions.Contains(file.Position))
                return Task.FromResult(System.IO.Stream.Null);
            var (path, tag1, tag2) = ((string, string, string))file.Tag;
            return Task.FromResult((Stream)new MemoryStream(source.GetBinaryReader(path).Func(r2 =>
            {
                r2.Position(file.Position);
                return file.Compressed != 0
                    ? r2.DecompressZlib((int)file.PackedSize, (int)file.FileSize)
                    : r2.ReadBytes((int)file.PackedSize);
            })));
        }
    }
}