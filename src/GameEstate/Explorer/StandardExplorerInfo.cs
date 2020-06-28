using GameEstate.Core;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats;
using GameEstate.Formats.Binary;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Explorer
{
    public static class StandardExplorerInfo
    {
        public static List<ExplorerInfoNode> GetFileInfo(ExplorerManager resource, FileMetadata file) =>
            new List<ExplorerInfoNode> {
                new ExplorerInfoNode { Name = $"Path: {file.Path}" },
                new ExplorerInfoNode { Name = $"FileSize: {file.FileSize}" },
            };

        public static async Task<List<ExplorerInfoNode>> GetDdsInfo(ExplorerManager resource, BinaryPakFile pakFile, FileMetadata file)
        {
            var data = await pakFile.LoadFileDataAsync(file);
            var texture = DdsReader.LoadDDSTexture(new MemoryStream(data));
            var textureInfo = new List<ExplorerInfoNode> {
                new ExplorerInfoNode { Name = $"Width: {texture.Width}" },
                new ExplorerInfoNode { Name = $"Height: {texture.Height}" },
                new ExplorerInfoNode { Name = $"Format: {texture.Format}" },
                new ExplorerInfoNode { Name = $"HasMipmaps: {texture.HasMipmaps}" },
            };
            return new List<ExplorerInfoNode> {
                new ExplorerInfoNode { Name = "File", Items = GetFileInfo(resource, file) },
                new ExplorerInfoNode { Name = "Texture", Items = textureInfo},
            };
        }
    }
}