using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GameEstate.Core
{
    [DebuggerDisplay("Dats: {Name} #{Dats.Count}")]
    public class MultiDatFile : AbstractDatFile
    {
        /// <summary>
        /// The dats
        /// </summary>
        public readonly IList<AbstractDatFile> DatFiles;

        public MultiDatFile(string game, string name, IList<AbstractDatFile> datFiles) : base(game, name) => DatFiles = datFiles;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose() => Close();

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public override void Close()
        {
            if (DatFiles != null)
                foreach (var datFile in DatFiles)
                    datFile.Close();
        }

        #region Explorer

        /// <summary>
        /// Gets the explorer item nodes.
        /// </summary>
        /// <param name="manager">The resource.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override async Task<List<ExplorerItemNode>> GetExplorerItemNodesAsync(ExplorerManager manager)
        {
            var root = new List<ExplorerItemNode>();
            foreach (var datFile in DatFiles)
                root.Add(new ExplorerItemNode(datFile.Name, manager.PackageIcon, items: await datFile.GetExplorerItemNodesAsync(manager)) { DatFile = datFile });
            return root;
        }

        /// <summary>
        /// Gets the explorer information nodes.
        /// </summary>
        /// <param name="manager">The resource.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Task<List<ExplorerInfoNode>> GetExplorerInfoNodesAsync(ExplorerManager manager, ExplorerItemNode item)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}