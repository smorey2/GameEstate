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
        public readonly IList<AbstractPakFile> Paks;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPakFile" /> class.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="paks">The packs.</param>
        public MultiPakFile(string game, IList<AbstractPakFile> paks) : base(game) => Paks = paks;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose() => Close();

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public override void Close()
        {
            if (Paks != null)
                foreach (var pak in Paks)
                    pak.Close();
        }

        /// <summary>
        /// Determines whether the specified file path contains file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        ///   <c>true</c> if the specified file path contains file; otherwise, <c>false</c>.
        /// </returns>
        public override bool Contains(string filePath) => Paks.Any(x => x.Contains(filePath));

        /// <summary>
        /// Loads the file data asynchronous.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        /// <exception cref="System.IO.FileNotFoundException">Could not find file \"{filePath}\".</exception>
        public override Task<byte[]> LoadFileDataAsync(string filePath, Action<FileMetadata, string> exception) =>
            (Paks.FirstOrDefault(x => x.Contains(filePath)) ?? throw new FileNotFoundException($"Could not find file \"{filePath}\"."))
            .LoadFileDataAsync(filePath, exception);
    }
}