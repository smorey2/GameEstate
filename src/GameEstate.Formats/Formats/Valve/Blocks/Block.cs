using GameEstate.Core;
using System;
using System.IO;

namespace GameEstate.Formats.Valve.Blocks
{
    public abstract class Block
    {
        /// <summary>
        /// Gets or sets the offset to the data.
        /// </summary>
        public uint Offset { get; set; }

        /// <summary>
        /// Gets or sets the data size.
        /// </summary>
        public uint Size { get; set; }

        public abstract void Read(BinaryPak parent, BinaryReader r);

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            using (var w = new IndentedTextWriter())
            {
                WriteText(w);
                return w.ToString();
            }
        }

        /// <summary>
        /// Writers the correct object to IndentedTextWriter.
        /// </summary>
        /// <param name="w">IndentedTextWriter.</param>
        public virtual void WriteText(IndentedTextWriter w) =>
            w.WriteLine("{0:X8}", Offset);

        public static Block Factory(BinaryPak source, string value)
        {
            switch (value)
            {
                case "DATA": return DATA.Factory(source);
                case "REDI": return new REDI();
                case "RERL": return new RERL();
                case "NTRO": return new NTRO();
                case "VBIB": return new VBIB();
                case "VXVS": return new VXVS();
                case "SNAP": return new SNAP();
                case "MBUF": return new MBUF();
                case "CTRL": return new CTRL();
                case "MDAT": return new MDAT();
                case "MRPH": return new MRPH();
                case "ANIM": return new ANIM();
                case "ASEQ": return new ASEQ();
                case "AGRP": return new AGRP();
                case "PHYS": return new PHYS();
                default: throw new ArgumentOutOfRangeException(nameof(value), $"Unrecognized block type '{value}'");
            }
        }
    }
}
