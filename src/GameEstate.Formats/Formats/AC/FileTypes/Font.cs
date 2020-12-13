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
    /// These are client_portal.dat files starting with 0x40.
    /// It is essentially a map to a specific texture file (spritemap) that contains all the characters in this font.
    /// </summary>
    [PakFileType(PakFileType.Font)]
    public class Font : AbstractFileType, IGetExplorerInfo
    {
        public readonly uint MaxCharHeight;
        public readonly uint MaxCharWidth;
        public readonly FontCharDesc[] CharDescs;
        public readonly uint NumHorizontalBorderPixels;
        public readonly uint NumVerticalBorderPixels;
        public readonly uint BaselineOffset;
        public readonly uint ForegroundSurfaceDataID; // This is a DataID to a Texture (0x06) type, if set
        public readonly uint BackgroundSurfaceDataID; // This is a DataID to a Texture (0x06) type, if set

        public Font(BinaryReader r)
        {
            Id = r.ReadUInt32();
            MaxCharHeight = r.ReadUInt32();
            MaxCharWidth = r.ReadUInt32();
            CharDescs = r.ReadL32Array(x => new FontCharDesc(x));
            NumHorizontalBorderPixels = r.ReadUInt32();
            NumVerticalBorderPixels = r.ReadUInt32();
            BaselineOffset = r.ReadUInt32();
            ForegroundSurfaceDataID = r.ReadUInt32();
            BackgroundSurfaceDataID = r.ReadUInt32();
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(Font)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                    //new ExplorerInfoNode($"Type: {Type}"),
                })
            };
            return nodes;
        }
    }
}
