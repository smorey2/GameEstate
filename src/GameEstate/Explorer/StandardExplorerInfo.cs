using GameEstate.Core;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats.Binary;
using GameEstate.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Explorer
{
    public static class StandardExplorerInfo
    {
        static Task<List<ExplorerInfoNode>> GetFileInfoAsync(ExplorerManager resource, FileMetadata file) => Task.FromResult(new List<ExplorerInfoNode> {
            new ExplorerInfoNode($"Path: {file.Path}"),
            new ExplorerInfoNode($"FileSize: {file.FileSize}"),
        });

        public static async Task<List<ExplorerInfoNode>> GetDdsAsync(ExplorerManager resource, BinaryPakFile pakFile, FileMetadata file)
        {
            var data = await pakFile.LoadFileDataAsync(file);
            var texture = new TextureInfo().LoadDdsTexture(new MemoryStream(data));
            var textureInfo = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"Width: {texture.Width}"),
                new ExplorerInfoNode($"Height: {texture.Height}"),
                new ExplorerInfoNode($"GLFormat: {texture.GLFormat}"),
                new ExplorerInfoNode($"Mipmaps: {texture.Mipmaps}"),
            };
            return new List<ExplorerInfoNode> {
                new ExplorerInfoNode(".texture", tag: texture),
                new ExplorerInfoNode("File", items: await GetFileInfoAsync(resource, file)),
                new ExplorerInfoNode("Texture", items: textureInfo),
            };
        }

        public static async Task<List<ExplorerInfoNode>> GetDefaultAsync(ExplorerManager resource, BinaryPakFile pakFile, FileMetadata file)
        {
            var data = await pakFile.LoadFileDataAsync(file);
            return new List<ExplorerInfoNode> {
                new ExplorerInfoNode(".generic", tag: data),
                new ExplorerInfoNode("File", items: await GetFileInfoAsync(resource, file)),
            };
        }
    }
}