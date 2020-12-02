//using GameEstate.Core;
//using System;
//using System.IO;

//namespace GameEstate.Formats.Red.Blocks
//{
//    public abstract class Block
//    {
//        /// <summary>
//        /// Gets or sets the offset to the data.
//        /// </summary>
//        public uint Offset { get; set; }

//        /// <summary>
//        /// Gets or sets the data size.
//        /// </summary>
//        public uint Size { get; set; }

//        public abstract void Read(BinaryPak parent, BinaryReader r);

//        public static Block Factory(BinaryPak source, string value)
//        {
//            switch (value)
//            {
//                case "DATA": return DATA.Factory(source);
//                case "REDI": return new REDI();
//                case "RERL": return new RERL();
//                case "NTRO": return new NTRO();
//                case "VBIB": return new VBIB_();
//                case "VXVS": return new VXVS();
//                case "SNAP": return new SNAP();
//                case "MBUF": return new MBUF();
//                case "CTRL": return new CTRL();
//                case "MDAT": return new MDAT();
//                case "MRPH": return new MRPH();
//                case "ANIM": return new ANIM();
//                case "ASEQ": return new ASEQ();
//                case "AGRP": return new AGRP();
//                case "PHYS": return new PHYS();
//                default: throw new ArgumentOutOfRangeException(nameof(value), $"Unrecognized block type '{value}'");
//            }
//        }
//    }
//}
