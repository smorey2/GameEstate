using GameEstate.Core.DataFormat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameEstate.Core
{
    [DebuggerDisplay("Paks: {Paks.Count}")]
    public class MultiPakFile : IDisposable
    {
        /// <summary>
        /// The paks
        /// </summary>
        public readonly IList<CorePakFile> Paks;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPakFile"/> class.
        /// </summary>
        /// <param name="paks">The packs.</param>
        public MultiPakFile(IList<CorePakFile> paks) => Paks = paks;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() => Close();

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
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
        public bool ContainsFile(string filePath) => Paks.Any(x => x.ContainsFile(filePath));

        /// <summary>
        /// Loads the file data asynchronous.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        /// <exception cref="System.IO.FileNotFoundException">Could not find file \"{filePath}\".</exception>
        public Task<byte[]> LoadFileDataAsync(string filePath, Action<FileMetadata, string> exception) =>
            (Paks.FirstOrDefault(x => x.ContainsFile(filePath)) ?? throw new FileNotFoundException($"Could not find file \"{filePath}\"."))
            .LoadFileDataAsync(filePath, exception);
    }
}