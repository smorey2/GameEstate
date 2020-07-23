using System;
using Xunit;

namespace GameEstate.CoreTests
{
    // Library
    // embeded: Compression.Doboz:DobozDecoder - PakBinaryRed
    // embeded: Compression:Lzf - PakBinaryRed
    // embeded: SevenZip.Compression.LZMA:Decoder - CompiledShader
    // K4os.Compression.LZ4:LZ4Codec - PakBinaryRed, DATABinaryKV3, DATATexture
    // ICSharpCode.SharpZipLib:Zip:ZipFile - PakBinaryZip (not a compression algorithm)
    // ICSharpCode.SharpZipLib:Zip.Compression.Streams:InflaterInputStream - PakBinaryRed, PakBinaryTes
    // ICSharpCode.SharpZipLib:Lzw:Lzw​Input​Stream - PakBinaryTes
    // Zstd.Net - not used

    public class CompressTest
    {
        [Theory]
        [InlineData("Red", "game:/base_scripts.dzip#Witcher2", "core/2darray.ws", 968)] // embeded: Compression:Lzf
        public void Lzf(string estate, string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData(estate, uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("Tes", "game:/Morrowind.bsa#Morrowind", "meshes/lavasteam.nif", 17725)]
        [InlineData("Red", "game:/main.key#Witcher", "2da00.bif", 887368)]
        [InlineData("Red", "game:/content0/bundles/xml.bundle#Witcher3", "engine/physics/apexclothmaterialpresets.xml", 2512)] // 0 - None
        public void None(string estate, string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData(estate, uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("Red", "game:/content0/bundles/xml.bundle#Witcher3", "engine/io/priority_table.xml", 8596)] // 1 - ICSharpCode.SharpZipLib:Zip.Compression.Streams:InflaterInputStream
        [InlineData("Tes", "game:/Oblivion - Meshes.bsa#Oblivion", "trees/treecottonwoodsu.spt", 1333)] // ICSharpCode.SharpZipLib:Zip.Compression.Streams:InflaterInputStream
        [InlineData("Tes", "game:/Skyrim - Meshes0.bsa#SkyrimSE", "meshes/scalegizmo.nif", 593998)] // ICSharpCode.SharpZipLib:Zip.Compression.Streams:InflaterInputStream
        [InlineData("Tes", "game:/Skyrim - Textures0.bsa#SkyrimSE", "textures/actors/dog/dog.dds", 593998)] // ICSharpCode.SharpZipLib:Zip.Compression.Streams:InflaterInputStream
        [InlineData("Tes", "game:/Fallout4 - Meshes.ba2#Fallout4", "Meshes/Marker_Error.NIF", 2334)] // ICSharpCode.SharpZipLib:Zip.Compression.Streams:InflaterInputStream
        public void Zlib(string estate, string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData(estate, uri, sampleFile, sampleFileSize);

        //[Theory]
        //[InlineData("Red", "game:/content0/bundles/xml.bundle#Witcher3", null, 0)] // 2
        //public void Snappy(string estate, string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData(estate, uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("Red", "game:/content0/bundles/xml.bundle#Witcher3", "gameplay/items/def_item_misc.xml", 29362)] // 3 - embeded: Doboz:DobozDecoder
        public void Doboz(string estate, string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData(estate, uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("Red", "game:/content0/bundles/xml.bundle#Witcher3", "gameplay/abilities/monster_base_abl.xml", 593998)] // 4,5 - K4os.Compression.LZ4:LZ4Codec
        //[InlineData("Valve", "game:/content0/bundles/xml.bundle#Witcher3", "gameplay/abilities/.xml", 0)] // K4os.Compression.LZ4:LZ4Codec
        //[InlineData("Valve", "game:/content0/bundles/xml.bundle#Witcher3", "gameplay/abilities/monster_base_abl.xml", 0)] // K4os.Compression.LZ4:LZ4Codec
        public void Lz4(string estate, string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData(estate, uri, sampleFile, sampleFileSize);

        [InlineData("Tes", "game:/content0/bundles/xml.bundle#Witcher3", "gameplay/abilities/monster_base_abl.xml", 593998)] // ICSharpCode.SharpZipLib:Lzw:Lzw​Input​Stream
        public void Lzw​(string estate, string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData(estate, uri, sampleFile, sampleFileSize);

        //[InlineData("Valve", "game:/content0/bundles/xml.bundle#Witcher3", "gameplay/abilities/monster_base_abl.xml", 593998)] // embeded: SevenZip.Compression.LZMA:Decoder
        //public void Lzma​(string estate, string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData(estate, uri, sampleFile, sampleFileSize);

        static void EstateLoadFileData(string estate, string uri, string sampleFile, int sampleFileSize)
        {
            if (sampleFile == null) return;
            var pakFile = EstateManager.GetEstate(estate).OpenPakFile(new Uri(uri));
            Assert.True(pakFile.Contains(sampleFile));
            Assert.Equal(sampleFileSize, pakFile.LoadFileDataAsync(sampleFile).Result.Length);
        }
    }
}
