using GameEstate.Formats.Binary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Core
{
    /// <summary>
    /// StreamDatFile
    /// </summary>
    /// <typeparam name="AbstractHost">The type of the bstract host.</typeparam>
    /// <seealso cref="GameEstate.Core.CoreDatFile" />
    public class StreamDatFile : CoreDatFile
    {
        readonly AbstractHost Host;

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamDatFile" /> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="game">The game.</param>
        /// <param name="address">The host.</param>
        public StreamDatFile(Func<Uri, string, AbstractHost> factory, string filePath, string game, Uri address = null) : base(filePath, game, new DatFormatStream())
        {
            UsePool = false;
            if (address != null)
                Host = factory(address, filePath);
            Open();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="StreamDatFile" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="game">The game.</param>
        /// <param name="filePath">The file path.</param>
        public StreamDatFile(CorePakFile parent, string game, string filePath) : base(filePath, game, new DatFormatStream())
        {
            UsePool = false;
            Open();
        }

        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <param name="_">The .</param>
        /// <param name="stage">The stage.</param>
        public override Task ReadAsync(BinaryReader _, DatFormat.ReadStage stage)
        {
            // http dak
            if (Host != null)
                return Task.CompletedTask;

            // read dak
            return Task.CompletedTask;
        }

        /// <summary>
        /// Writes the asynchronous.
        /// </summary>
        /// <param name="_">The .</param>
        /// <param name="stage">The stage.</param>
        /// <exception cref="NotSupportedException"></exception>
        public override Task WriteAsync(BinaryWriter _, DatFormat.WriteStage stage)
        {
            // http pak
            if (Host != null)
                throw new NotSupportedException();

            // write pak
            return Task.CompletedTask;
        }
    }
}