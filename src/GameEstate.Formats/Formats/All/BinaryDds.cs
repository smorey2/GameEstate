using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Graphics.DirectX;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Formats.All
{
    public class BinaryDds : IGetExplorerInfo
    {
        public BinaryDds() { }
        public BinaryDds(BinaryReader r) => Read(r);
        public static Task<object> Factory(BinaryReader r, FileMetadata f) => Task.FromResult((object)new BinaryDds(r));

        public DDS_HEADER Header;
        public DDS_HEADER_DXT10? HeaderDXT10;

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file) => new List<ExplorerInfoNode> {
            new ExplorerInfoNode(null, new ExplorerContentTab { Type = "Texture", Name = Path.GetFileName(file.Path), Value = this }),
            new ExplorerInfoNode("DDS Texture", items: new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"Width: {Header.dwWidth}"),
                new ExplorerInfoNode($"Height: {Header.dwHeight}"),
                new ExplorerInfoNode($"Mipmaps: {Header.dwMipMapCount}"),
            }),
        };

        public unsafe void Read(BinaryReader r)
        {
            var magic = r.ReadUInt32();
            if (magic != DDS_HEADER.Literal.DDS_)
                throw new FileFormatException($"Invalid DDS file magic: \"{magic}\".");
            Header = r.ReadT<DDS_HEADER>(DDS_HEADER.SizeOf);
            HeaderDXT10 = Header.ddspf.dwFourCC == DDS_HEADER.Literal.DX10
                ? (DDS_HEADER_DXT10?)r.ReadT<DDS_HEADER_DXT10>(DDS_HEADER_DXT10.SizeOf)
                : null;
            Header.Verify();
        }
    }
}