using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats.Binary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameEstate.Core
{
    [DebuggerDisplay("Paks: {Paks.Count}")]
    public class MultiPakFile : AbstractPakFile
    {
        /// <summary>
        /// The paks
        /// </summary>
        public readonly IList<AbstractPakFile> PakFiles;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPakFile" /> class.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="name">The name.</param>
        /// <param name="pakFiles">The packs.</param>
        public MultiPakFile(string game, string name, IList<AbstractPakFile> pakFiles) : base(game, name) => PakFiles = pakFiles;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose() => Close();

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public override void Close()
        {
            if (PakFiles != null)
                foreach (var pakFile in PakFiles)
                    pakFile.Close();
        }

        /// <summary>
        /// Determines whether the specified file path contains file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        ///   <c>true</c> if the specified file path contains file; otherwise, <c>false</c>.
        /// </returns>
        public override bool Contains(string filePath) => PakFiles.Any(x => x.Contains(filePath));

        /// <summary>
        /// Loads the file data asynchronous.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        /// <exception cref="System.IO.FileNotFoundException">Could not find file \"{filePath}\".</exception>
        public override Task<byte[]> LoadFileDataAsync(string filePath, Action<FileMetadata, string> exception) =>
            (PakFiles.FirstOrDefault(x => x.Contains(filePath)) ?? throw new FileNotFoundException($"Could not find file \"{filePath}\"."))
            .LoadFileDataAsync(filePath, exception);

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
            foreach (var pakFile in PakFiles)
                root.Add(new ExplorerItemNode(pakFile.Name, manager.PackageIcon, items: await pakFile.GetExplorerItemNodesAsync(manager)) { PakFile = pakFile });
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