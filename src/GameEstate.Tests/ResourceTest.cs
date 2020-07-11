using GameEstate.Core;
using System;
using Xunit;

namespace GameEstate.CoreTests
{
    public class ResourceTest
    {
        [Theory]
        // game
        [InlineData(false, "Tes", "game:/Oblivion*.bsa#Oblivion", "Oblivion", false, 6, "Oblivion - Meshes.bsa", "trees/treeginkgo.spt", 2059)]
        [InlineData(true, "Tes", "game:/Oblivion*.bsa/#Oblivion", "Oblivion", false, 6, "Oblivion - Meshes.bsa", "trees/treeginkgo.spt", 2059)]
        // file: file
        [InlineData(false, "Tes", "file:///C:/Program%20Files%20(x86)/Steam/steamapps/common/Oblivion/Data/Oblivion*.bsa#Oblivion", "Oblivion", false, 6, "Oblivion - Meshes.bsa", "trees/treeginkgo.spt", 2059)]
        [InlineData(false, "Tes", "file:///C:/Program%20Files%20(x86)/Steam/steamapps/common/Oblivion/Data/Oblivion%20-%20Meshes.bsa#Oblivion", "Oblivion", false, 1, "Oblivion - Meshes.bsa", "trees/treeginkgo.spt", 2059)]
        // file: folder
        [InlineData(false, "Tes", "file:///D:/T_/Oblivion/Oblivion*.bsa/#Oblivion", "Oblivion", true, 6, "Oblivion - Meshes.bsa", "trees/treeginkgo.spt", 2059)]
        [InlineData(false, "Tes", "file:///D:/T_/Oblivion/Oblivion%20-%20Meshes.bsa/#Oblivion", "Oblivion", true, 1, "Oblivion - Meshes.bsa", "trees/treeginkgo.spt", 2059)]
        // http
        [InlineData(true, "Tes", "http://192.168.1.3/Estates/Oblivion/Oblivion*.bsa#Oblivion", "Oblivion", true, 6, "Oblivion - Meshes.bsa", "trees/treeginkgo.spt", 2059)]
        [InlineData(false, "Tes", "http://192.168.1.3/Estates/Oblivion/Oblivion*.bsa/#Oblivion", "Oblivion", true, 6, "Oblivion - Meshes.bsa", "trees/treeginkgo.spt", 2059)]
        [InlineData(false, "Tes", "http://192.168.1.3/Estates/Oblivion/Oblivion%20-%20Meshes.bsa/#Oblivion", "Oblivion", true, 1, "Oblivion - Meshes.bsa", "trees/treeginkgo.spt", 2059)]
        public void Resource(bool shouldThrow, string estateName, string uri, string game, bool filePak, int paks, string firstPak, string sampleFile, int sampleFileSize)
        {
            var estate = EstateManager.GetEstate(estateName);
            if (shouldThrow)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => estate.ParseResource(new Uri(uri)));
                return;
            }
            var resource = estate.ParseResource(new Uri(uri));
            Assert.Equal(game, resource.Game);
            Assert.Equal(filePak, resource.StreamPak);
            Assert.Equal(paks, resource.Paths.Length);
            // multiPak
            var multiPak = estate.OpenPakFile(new Uri(uri)) as MultiPakFile;
            if (multiPak == null)
                throw new InvalidOperationException("multiPak not a MultiPakFile");
            Assert.Equal(paks, multiPak.PakFiles.Count);
            var pak = multiPak.PakFiles[0] as BinaryPakFile;
            if (pak == null)
                throw new InvalidOperationException("pak not a BinaryPakFile");
            Assert.Equal(firstPak, pak.Name);
            Assert.True(pak.Contains(sampleFile));
            Assert.Equal(sampleFileSize, pak.LoadFileDataAsync(sampleFile).Result.Length);
        }
    }
}
