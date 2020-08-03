using System;
using Xunit;

namespace GameEstate.Tests.PakFiles
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
        [InlineData("Tes", "game:/SeventySix - 00UpdateMain.ba2#Fallout76", "meshes/actors/moleminer/treasurehunter/treasurehunter.ztl", 17725)]
        public void Error(string estate, string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData(estate, uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("Tes", "game:/Morrowind.bsa#Morrowind", "meshes/lavasteam.nif", 17725)]
        [InlineData("Red", "game:/main.key#Witcher", "2da00.bif", 887368)]
        [InlineData("Red", "game:/content0/bundles/xml.bundle#Witcher3", "engine/physics/apexclothmaterialpresets.xml", 2512)] // 0 - None
        public void None(string estate, string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData(estate, uri, sampleFile, sampleFileSize);

        [Theory] // embeded: Compression:Lzf
        [InlineData("Red", "game:/base_scripts.dzip#Witcher2", "core/2darray.ws", 968)]
        public void DecompressLzf(string estate, string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData(estate, uri, sampleFile, sampleFileSize);

        [Theory] // ICSharpCode.SharpZipLib:Zip.Compression.Streams:InflaterInputStream
        [InlineData("Arkane", "game:/game1.index#Dishonored2", "strings/english_m.lang", 968)]
        [InlineData("Red", "game:/content0/bundles/xml.bundle#Witcher3", "engine/io/priority_table.xml", 8596)] // 1
        [InlineData("Tes", "game:/Oblivion - Meshes.bsa#Oblivion", "trees/treecottonwoodsu.spt", 62296)]
        [InlineData("Tes", "game:/Skyrim - Textures.bsa#Skyrim", "textures/actors/dog/dog.dds", 1398256)]
        [InlineData("Tes", "game:/Fallout4 - Meshes.ba2#Fallout4", "Meshes/Marker_Error.NIF", 2334)]
        public void DecompressZlib(string estate, string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData(estate, uri, sampleFile, sampleFileSize);

        //[Theory]
        //[InlineData("Red", "game:/content0/bundles/xml.bundle#Witcher3", null, 0)] // 2
        //public void DecompressSnappy(string estate, string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData(estate, uri, sampleFile, sampleFileSize);

        [Theory] //  embeded: Doboz:DobozDecoder
        [InlineData("Red", "game:/content0/bundles/xml.bundle#Witcher3", "gameplay/items/def_item_misc.xml", 29362)] // 3
        public void DecompressDoboz(string estate, string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData(estate, uri, sampleFile, sampleFileSize);

        [Theory] // K4os.Compression.LZ4:LZ4Codec
        [InlineData("Red", "game:/content0/bundles/xml.bundle#Witcher3", "gameplay/abilities/monster_base_abl.xml", 591546)] // 4,5
        //[InlineData("Valve", "game:/content0/bundles/xml.bundle#Witcher3", "gameplay/abilities/.xml", 0)]
        //[InlineData("Valve", "game:/content0/bundles/xml.bundle#Witcher3", "gameplay/abilities/monster_base_abl.xml", 0)]
        [InlineData("Tes", "game:/Skyrim - Meshes0.bsa#SkyrimSE", "meshes/scalegizmo.nif", 8137)]
        [InlineData("Tes", "game:/Skyrim - Textures0.bsa#SkyrimSE", "textures/actors/dog/dog.dds", 1398240)]
        public void DecompressLz4(string estate, string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData(estate, uri, sampleFile, sampleFileSize);

        [Theory] // ICSharpCode.SharpZipLib:Lzw:Lzw​Input​Stream
        [InlineData("Tes", "game:/content0/bundles/xml.bundle#Witcher2", "gameplay/abilities/monster_base_abl.xml", 593998)] 
        public void DecompressLzw​(string estate, string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData(estate, uri, sampleFile, sampleFileSize);

        [Theory] // embeded: SevenZip.Compression.LZMA:Decoder
        [InlineData("Valve", "game:/content0/bundles/xml.bundle#Witcher3", "gameplay/abilities/monster_base_abl.xml", 593998)] 
        public void DecompressLzma​(string estate, string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData(estate, uri, sampleFile, sampleFileSize);

        static void EstateLoadFileData(string estate, string uri, string sampleFile, int sampleFileSize)
        {
            if (sampleFile == null) return;
            var pakFile = EstateManager.GetEstate(estate).OpenPakFile(new Uri(uri));
            Assert.True(pakFile.Contains(sampleFile));
            Assert.Equal(sampleFileSize, pakFile.LoadFileDataAsync(sampleFile).Result.Length);
        }
    }
}
