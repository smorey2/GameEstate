using GameEstate.Core;
using GameEstate.Toy.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace GameEstate.Toy.Models
{
    public class Texture // : ResourceData
    {
        const short MipmapLevelToExtract = 0; // for debugging purposes

        public class SpritesheetData
        {
            public class Sequence
            {
                public class Frame
                {
                    public Vector2 StartMins { get; set; }
                    public Vector2 StartMaxs { get; set; }

                    public Vector2 EndMins { get; set; }
                    public Vector2 EndMaxs { get; set; }
                }

                public Frame[] Frames { get; set; }

                public float FramesPerSecond { get; set; }
            }

            public Sequence[] Sequences { get; set; }
        }

        //BinaryReader Reader;
        //long DataOffset;
        //Resource Resource;

        public ushort Version { get; private set; }

        public ushort Width { get; private set; }

        public ushort Height { get; private set; }

        public ushort Depth { get; private set; }

        public float[] Reflectivity { get; private set; }

        public VTexFlags Flags { get; private set; }

        public VTexFormat Format { get; private set; }

        public byte NumMipLevels { get; private set; }

        public uint Picmip0Res { get; private set; }

        public Dictionary<VTexExtraData, byte[]> ExtraData { get; private set; } = new Dictionary<VTexExtraData, byte[]>();

        public ushort NonPow2Width { get; private set; }

        public ushort NonPow2Height { get; private set; }

        //int[] CompressedMips;
        //bool IsActuallyCompressedMips;

        public SpritesheetData GetSpriteSheetData()
        {
            return null;
        }

        public override string ToString()
        {
            using (var writer = new IndentedTextWriter())
            {
                writer.WriteLine("{0,-12} = {1}", "VTEX Version", Version);
                writer.WriteLine("{0,-12} = {1}", "Width", Width);
                writer.WriteLine("{0,-12} = {1}", "Height", Height);
                writer.WriteLine("{0,-12} = {1}", "Depth", Depth);
                writer.WriteLine("{0,-12} = {1}", "NonPow2W", NonPow2Width);
                writer.WriteLine("{0,-12} = {1}", "NonPow2H", NonPow2Height);
                writer.WriteLine("{0,-12} = ( {1:F6}, {2:F6}, {3:F6}, {4:F6} )", "Reflectivity", Reflectivity[0], Reflectivity[1], Reflectivity[2], Reflectivity[3]);
                writer.WriteLine("{0,-12} = {1}", "NumMipLevels", NumMipLevels);
                writer.WriteLine("{0,-12} = {1}", "Picmip0Res", Picmip0Res);
                writer.WriteLine("{0,-12} = {1} (VTEX_FORMAT_{2})", "Format", (int)Format, Format);
                writer.WriteLine("{0,-12} = 0x{1:X8}", "Flags", (int)Flags);

                foreach (Enum value in Enum.GetValues(Flags.GetType()))
                {
                    if (Flags.HasFlag(value))
                    {
                        writer.WriteLine("{0,-12} | 0x{1:X8} = VTEX_FLAG_{2}", string.Empty, Convert.ToInt32(value), value);
                    }
                }

                writer.WriteLine("{0,-12} = {1} entries:", "Extra Data", ExtraData.Count);

                var entry = 0;

                foreach (var b in ExtraData)
                {
                    writer.WriteLine("{0,-12}   [ Entry {1}: VTEX_EXTRA_DATA_{2} - {3} bytes ]", string.Empty, entry++, b.Key, b.Value.Length);

                    //if (b.Key == VTexExtraData.COMPRESSED_MIP_SIZE)
                    //{
                    //    writer.WriteLine("{0,-16}   [ {1} mips, sized: {2} ]", string.Empty, CompressedMips.Length, string.Join(", ", CompressedMips));
                    //}
                }

                //for (var j = 0; j < NumMipLevels; j++)
                //{
                //    writer.WriteLine($"Mip level {j} - buffer size: {CalculateBufferSizeForMipLevel(j)}");
                //}

                return writer.ToString();
            }
        }
    }
}
