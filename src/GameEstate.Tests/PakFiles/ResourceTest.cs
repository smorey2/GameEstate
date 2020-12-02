using GameEstate.Core;
using System;
using Xunit;

namespace GameEstate.Tests.PakFiles
{
    public class ResourceTest
    {
        static ResourceTest() => EstatePlatform.Startups.Add(TestPlatform.Startup);

        const string OblivionFile = "file:///D:/Program%20Files%20(x86)/Steam/steamapps/common/Oblivion";
        const string OblivionFolder = "file:////192.168.1.3/User/_SERVE/Assets/Oblivion";
        const string OblivionHttp = "http://192.168.1.3/Estates/Oblivion";

        [Theory]
        // game
        [InlineData(false, "Tes", "game:/Oblivion*.bsa#Oblivion", "Oblivion", false, 6, "Oblivion - Meshes.bsa", "trees/treeginkgo.spt", 2059)]
        [InlineData(true, "Tes", "game:/Oblivion*.bsa/#Oblivion", "Oblivion", false, 6, "Oblivion - Meshes.bsa", "trees/treeginkgo.spt", 2059)]
        // file: file
        [InlineData(false, "Tes", OblivionFile + "/Data/Oblivion*.bsa#Oblivion", "Oblivion", false, 6, "Oblivion - Meshes.bsa", "trees/treeginkgo.spt", 2059)]
        [InlineData(false, "Tes", OblivionFile + "/Data/Oblivion%20-%20Meshes.bsa#Oblivion", "Oblivion", false, 1, "Oblivion - Meshes.bsa", "trees/treeginkgo.spt", 2059)]
        // file: folder
        //[InlineData(false, "Tes", OblivionFolder + "/Oblivion*.bsa/#Oblivion", "Oblivion", true, 6, "Oblivion - Meshes.bsa", "trees/treeginkgo.spt", 2059)]
        //[InlineData(false, "Tes", OblivionFolder + "/Oblivion%20-%20Meshes.bsa/#Oblivion", "Oblivion", true, 1, "Oblivion - Meshes.bsa", "trees/treeginkgo.spt", 2059)]
        // http
        [InlineData(true, "Tes", OblivionHttp + "/Oblivion*.bsa#Oblivion", "Oblivion", true, 6, "Oblivion - Meshes.bsa", "trees/treeginkgo.spt", 2059)]
        [InlineData(false, "Tes", OblivionHttp + "/Oblivion*.bsa/#Oblivion", "Oblivion", true, 6, "Oblivion - Meshes.bsa", "trees/treeginkgo.spt", 2059)]
        [InlineData(false, "Tes", OblivionHttp + "/Oblivion%20-%20Meshes.bsa/#Oblivion", "Oblivion", true, 1, "Oblivion - Meshes.bsa", "trees/treeginkgo.spt", 2059)]
        public void Resource(bool shouldThrow, string estateName, string uri, string game, bool streamPak, int pathsFound, string firstPak, string sampleFile, int sampleFileSize)
        {
            var estate = EstateManager.GetEstate(estateName);
            if (shouldThrow)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => estate.ParseResource(new Uri(uri)));
                return;
            }
            var resource = estate.ParseResource(new Uri(uri));
            Assert.Equal(game, resource.Game);
            Assert.Equal(streamPak, resource.StreamPak);
            Assert.Equal(pathsFound, resource.Paths.Length);
            var pakFile = estate.OpenPakFile(new Uri(uri));
            if (pakFile is MultiPakFile multiPakFile)
            {
                Assert.Equal(pathsFound, multiPakFile.PakFiles.Count);
                pakFile = multiPakFile.PakFiles[0];
            }
            if (pakFile == null)
                throw new InvalidOperationException("pak not opened");
            Assert.Equal(firstPak, pakFile.Name);
            Assert.True(pakFile.Contains(sampleFile));
            Assert.Equal(sampleFileSize, pakFile.LoadFileDataAsync(sampleFile).Result.Length);
        }
    }
}
