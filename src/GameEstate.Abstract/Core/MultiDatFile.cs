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
        public readonly IList<AbstractDatFile> Dats;

        public MultiDatFile(string game, string name, IList<AbstractDatFile> dats) : base(game, name) => Dats = dats;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose() => Close();

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public override void Close()
        {
            if (Dats != null)
                foreach (var dat in Dats)
                    dat.Close();
        }

        #region Explorer

        /// <summary>
        /// Gets the explorer item nodes.
        /// </summary>
        /// <param name="manager">The resource.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Task<List<ExplorerItemNode>> GetExplorerItemNodesAsync(ExplorerManager manager)
        {
            throw new NotImplementedException();
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