using System.Collections.Generic;
using System.Diagnostics;

namespace GameEstate.Core
{
    [DebuggerDisplay("Dats: {Dats.Count}")]
    public class MultiDatFile : AbstractDatFile
    {
        /// <summary>
        /// The dats
        /// </summary>
        public readonly IList<AbstractDatFile> Dats;

        public MultiDatFile(string game, IList<AbstractDatFile> dats) : base(game) => Dats = dats;

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
    }
}