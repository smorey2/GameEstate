using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Props;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.FileTypes
{
    [PakFileType(PakFileType.Texture)]
    public class Texture : AbstractFileType, IGetExplorerInfo
    {
        public readonly int Unknown;
        public readonly int Width;
        public readonly int Height;
        public readonly SurfacePixelFormat Format;
        public readonly int Length;
        public readonly byte[] SourceData;
        public readonly uint? DefaultPaletteId;
        // Used to store a custom palette. Each Key represents the PaletteIndex and the Value is the color.
        // This is used if you want to apply a non-default Palette to the image prior to extraction
        public Dictionary<int, uint> CustomPaletteColors;

        public Texture(BinaryReader r)
        {
            Id = r.ReadUInt32();
            Unknown = r.ReadInt32();
            Width = r.ReadInt32();
            Height = r.ReadInt32();
            Format = (SurfacePixelFormat)r.ReadUInt32();
            Length = r.ReadInt32();
            SourceData = r.ReadBytes(Length);
            switch (Format)
            {
                case SurfacePixelFormat.PFID_INDEX16:
                case SurfacePixelFormat.PFID_P8: DefaultPaletteId = r.ReadUInt32(); break;
                default: DefaultPaletteId = null; break;
            }
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(Texture)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                    new ExplorerInfoNode($"Unknown: {Unknown}"),
                    new ExplorerInfoNode($"Width: {Width}"),
                    new ExplorerInfoNode($"Height: {Height}"),
                    new ExplorerInfoNode($"Type: {Format}"),
                    new ExplorerInfoNode($"Size: {Length} bytes"),
                })
            };
            return nodes;
        }
    }

    /*

            /// <summary>
            /// Exports RenderSurface to a image file
            /// </summary>
            public void ExportTexture(string directory)
            {
                if (Length == 0) return;

                switch (Format)
                {
                    case SurfacePixelFormat.PFID_CUSTOM_RAW_JPEG:
                        {
                            var filename = Path.Combine(directory, $"{Id:X8}.jpg");
                            using (var w = new BinaryWriter(File.Open(filename, FileMode.Create)))
                                w.Write(SourceData);
                        }
                        break;
                    default:
                        {
                            var bitmapImage = GetBitmap();
                            var filename = Path.Combine(directory, $"{Id:X8}.png");
                            bitmapImage.Save(filename, ImageFormat.Png);
                        }
                        break;
                }
            }

            /// <summary>
            /// Reads RenderSurface to bitmap structure
            /// </summary>
            public Bitmap GetBitmap()
            {
                switch (Format)
                {
                    case SurfacePixelFormat.PFID_CUSTOM_RAW_JPEG:
                        {
                            var stream = new MemoryStream(SourceData);
                            var image = Image.FromStream(stream);
                            return new Bitmap(image);
                        }
                    case SurfacePixelFormat.PFID_DXT1:
                        {
                            var image = DxtUtil.DecompressDxt1(SourceData, Width, Height);
                            return GetBitmap(image);
                        }
                    case SurfacePixelFormat.PFID_DXT3:
                        {
                            var image = DxtUtil.DecompressDxt3(SourceData, Width, Height);
                            return GetBitmap(image);
                        }
                    case SurfacePixelFormat.PFID_DXT5:
                        {
                            var image = DxtUtil.DecompressDxt5(SourceData, Width, Height);
                            return GetBitmap(image);
                        }
                    default:
                        {
                            List<int> colors = GetImageColorArray();
                            return GetBitmap(colors);
                        }
                }
            }

            /// <summary>
            /// Converts the byte array SourceData into color values per pixel
            /// </summary>
            List<int> GetImageColorArray()
            {
                List<int> colors = new List<int>();
                if (Length == 0) return colors;

                switch (Format)
                {
                    case SurfacePixelFormat.PFID_R8G8B8: // RGB
                        using (BinaryReader reader = new BinaryReader(new MemoryStream(SourceData)))
                        {
                            for (uint i = 0; i < Height; i++)
                                for (uint j = 0; j < Width; j++)
                                {
                                    byte b = reader.ReadByte();
                                    byte g = reader.ReadByte();
                                    byte r = reader.ReadByte();
                                    int color = (r << 16) | (g << 8) | b;
                                    colors.Add(color);
                                }
                        }
                        break;
                    case SurfacePixelFormat.PFID_CUSTOM_LSCAPE_R8G8B8:
                        using (BinaryReader reader = new BinaryReader(new MemoryStream(SourceData)))
                        {
                            for (uint i = 0; i < Height; i++)
                                for (uint j = 0; j < Width; j++)
                                {
                                    byte r = reader.ReadByte();
                                    byte g = reader.ReadByte();
                                    byte b = reader.ReadByte();
                                    int color = (r << 16) | (g << 8) | b;
                                    colors.Add(color);
                                }
                        }
                        break;
                    case SurfacePixelFormat.PFID_A8R8G8B8: // ARGB format. Most UI textures fall into this category
                        using (BinaryReader reader = new BinaryReader(new MemoryStream(SourceData)))
                        {
                            for (uint i = 0; i < Height; i++)
                                for (uint j = 0; j < Width; j++)
                                    colors.Add(reader.ReadInt32());
                        }
                        break;
                    case SurfacePixelFormat.PFID_INDEX16: // 16-bit indexed colors. Index references position in a palette;
                        using (BinaryReader reader = new BinaryReader(new MemoryStream(SourceData)))
                        {
                            for (uint y = 0; y < Height; y++)
                                for (uint x = 0; x < Width; x++)
                                    colors.Add(reader.ReadInt16());
                        }
                        break;
                    case SurfacePixelFormat.PFID_A8: // Greyscale, also known as Cairo A8.
                    case SurfacePixelFormat.PFID_CUSTOM_LSCAPE_ALPHA:
                        using (BinaryReader reader = new BinaryReader(new MemoryStream(SourceData)))
                        {
                            for (uint y = 0; y < Height; y++)
                                for (uint x = 0; x < Width; x++)
                                    colors.Add(reader.ReadByte());
                        }
                        break;
                    case SurfacePixelFormat.PFID_P8: // Indexed
                        using (BinaryReader reader = new BinaryReader(new MemoryStream(SourceData)))
                        {
                            for (uint y = 0; y < Height; y++)
                                for (uint x = 0; x < Width; x++)
                                    colors.Add(reader.ReadByte());
                        }
                        break;
                    case SurfacePixelFormat.PFID_R5G6B5: // 16-bit RGB
                        using (BinaryReader reader = new BinaryReader(new MemoryStream(SourceData)))
                        {
                            for (uint y = 0; y < Height; y++)
                                for (uint x = 0; x < Width; x++)
                                {
                                    ushort val = reader.ReadUInt16();
                                    List<int> color = get565RGB(val);
                                    colors.Add(color[0]); // Red
                                    colors.Add(color[1]); // Green
                                    colors.Add(color[2]); // Blue
                                }
                        }
                        break;
                    case SurfacePixelFormat.PFID_A4R4G4B4:
                        using (BinaryReader reader = new BinaryReader(new MemoryStream(SourceData)))
                        {
                            for (uint y = 0; y < Height; y++)
                                for (uint x = 0; x < Width; x++)
                                {
                                    ushort val = reader.ReadUInt16();
                                    int alpha = (val >> 12) / 0xF * 255;
                                    int red = (val >> 8 & 0xF) / 0xF * 255;
                                    int green = (val >> 4 & 0xF) / 0xF * 255;
                                    int blue = (val & 0xF) / 0xF * 255;

                                    colors.Add(alpha);
                                    colors.Add(red);
                                    colors.Add(green);
                                    colors.Add(blue);
                                }
                        }
                        break;
                    default:
                        Console.WriteLine("Unhandled SurfacePixelFormat (" + Format.ToString() + ") in RenderSurface " + Id.ToString("X8"));
                        break;
                }

                return colors;
            }

            private List<int> GetPaletteIndexes()
            {
                List<int> colors = new List<int>();
                using (BinaryReader reader = new BinaryReader(new MemoryStream(SourceData)))
                {
                    for (uint y = 0; y < Height; y++)
                        for (uint x = 0; x < Width; x++)
                            colors.Add(reader.ReadInt16());
                }
                return colors;
            }

            /// <summary>
            /// Generates Bitmap data from colorArray.
            /// </summary>
            private Bitmap GetBitmap(List<int> colorArray)
            {
                Bitmap image = new Bitmap(Width, Height);
                switch (this.Format)
                {
                    case SurfacePixelFormat.PFID_R8G8B8:
                    case SurfacePixelFormat.PFID_CUSTOM_LSCAPE_R8G8B8:
                        for (int i = 0; i < Height; i++)
                            for (int j = 0; j < Width; j++)
                            {
                                int idx = (i * Width) + j;
                                int r = (colorArray[idx] & 0xFF0000) >> 16;
                                int g = (colorArray[idx] & 0xFF00) >> 8;
                                int b = colorArray[idx] & 0xFF;
                                image.SetPixel(j, i, Color.FromArgb(r, g, b));
                            }
                        break;
                    case SurfacePixelFormat.PFID_A8R8G8B8:
                        for (int i = 0; i < Height; i++)
                            for (int j = 0; j < Width; j++)
                            {
                                int idx = (i * Width) + j;
                                int a = (int)((colorArray[idx] & 0xFF000000) >> 24);
                                int r = (colorArray[idx] & 0xFF0000) >> 16;
                                int g = (colorArray[idx] & 0xFF00) >> 8;
                                int b = colorArray[idx] & 0xFF;
                                image.SetPixel(j, i, Color.FromArgb(a, r, g, b));
                            }
                        break;
                    case SurfacePixelFormat.PFID_INDEX16:
                    case SurfacePixelFormat.PFID_P8:
                        Palette pal = DatManager.PortalDat.ReadFromDat<Palette>((uint)DefaultPaletteId);

                        // Apply any custom palette colors, if any, to our loaded palette (note, this may be all of them!)
                        if (CustomPaletteColors.Count > 0)
                            foreach (KeyValuePair<int, uint> entry in CustomPaletteColors)
                                if (entry.Key <= pal.Colors.Count)
                                    pal.Colors[entry.Key] = entry.Value;

                        for (int i = 0; i < Height; i++)
                            for (int j = 0; j < Width; j++)
                            {
                                int idx = (i * Width) + j;
                                int a = (int)((pal.Colors[colorArray[idx]] & 0xFF000000) >> 24);
                                int r = (int)(pal.Colors[colorArray[idx]] & 0xFF0000) >> 16;
                                int g = (int)(pal.Colors[colorArray[idx]] & 0xFF00) >> 8;
                                int b = (int)pal.Colors[colorArray[idx]] & 0xFF;
                                image.SetPixel(j, i, Color.FromArgb(a, r, g, b));
                            }
                        break;
                    case SurfacePixelFormat.PFID_A8:
                    case SurfacePixelFormat.PFID_CUSTOM_LSCAPE_ALPHA:
                        for (int i = 0; i < Height; i++)
                            for (int j = 0; j < Width; j++)
                            {
                                int idx = (i * Width) + j;
                                int r = colorArray[idx];
                                int g = colorArray[idx];
                                int b = colorArray[idx];
                                image.SetPixel(j, i, Color.FromArgb(r, g, b));
                            }
                        break;
                    case SurfacePixelFormat.PFID_R5G6B5: // 16-bit RGB
                        for (int i = 0; i < Height; i++)
                            for (int j = 0; j < Width; j++)
                            {
                                int idx = 3 * ((i * Width) + j);
                                int r = (int)(colorArray[idx]);
                                int g = (int)(colorArray[idx + 1]);
                                int b = (int)(colorArray[idx + 2]);
                                image.SetPixel(j, i, Color.FromArgb(r, g, b));
                            }
                        break;
                    case SurfacePixelFormat.PFID_A4R4G4B4:
                        for (int i = 0; i < Height; i++)
                            for (int j = 0; j < Width; j++)
                            {
                                int idx = 4 * ((i * Width) + j);
                                int a = (colorArray[idx]);
                                int r = (colorArray[idx + 1]);
                                int g = (colorArray[idx + 2]);
                                int b = (colorArray[idx + 3]);
                                image.SetPixel(j, i, Color.FromArgb(a, r, g, b));
                            }
                        break;
                }
                return image;
            }

            /// <summary>
            /// Generates Bitmap data from byteArray, generated by DXT1, DXT3, and DXT5 image foramts.
            /// </summary>
            private Bitmap GetBitmap(byte[] byteArray)
            {
                Bitmap image = new Bitmap(Width, Height);
                for (int i = 0; i < Height; i++)
                    for (int j = 0; j < Width; j++)
                    {
                        int idx = 4 * ((i * Width) + j);
                        int r = (int)(byteArray[idx]);
                        int g = (int)(byteArray[idx + 1]);
                        int b = (int)(byteArray[idx + 2]);
                        int a = (int)(byteArray[idx + 3]);
                        image.SetPixel(j, i, Color.FromArgb(a, r, g, b));
                    }

                return image;
            }

            // https://docs.microsoft.com/en-us/windows/desktop/DirectShow/working-with-16-bit-rgb
            private List<int> get565RGB(ushort val)
            {
                List<int> color = new List<int>();

                int red_mask = 0xF800;
                int green_mask = 0x7E0;
                int blue_mask = 0x1F;

                int red = ((val & red_mask) >> 11) << 3;
                int green = ((val & green_mask) >> 5) << 2;
                int blue = (val & blue_mask) << 3;

                color.Add(red); // Red
                color.Add(green); // Green
                color.Add(blue); // Blue

                return color;
            }
    */
}
