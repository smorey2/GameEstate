using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats.Binary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameEstate.Estates
{
    public class ValvePakFile : AbstractPakFile
    {
        public ValvePakFile(string game, string name) : base(game, name)
        {
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override bool Contains(string filePath)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override Task<byte[]> LoadFileDataAsync(string filePath, Action<FileMetadata, string> exception)
        {
            throw new NotImplementedException();
        }

        #region Explorer

        /// <summary>
        /// Gets the explorer item nodes.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Task<List<ExplorerItemNode>> GetExplorerItemNodesAsync(ExplorerManager resource)
        {
            return Task.FromResult<List<ExplorerItemNode>>(null);
        }

        /// <summary>
        /// Gets the explorer information nodes.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Task<List<ExplorerInfoNode>> GetExplorerInfoNodesAsync(ExplorerManager resource, ExplorerItemNode item)
        {
            return Task.FromResult<List<ExplorerInfoNode>>(null);
        }

        #endregion
    }
}