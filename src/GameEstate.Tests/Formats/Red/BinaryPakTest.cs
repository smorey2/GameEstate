using GameEstate.Formats.Red;
using System;
using Xunit;

namespace GameEstate.Tests.Formats.Red
{
    public class BinaryPakTest
    {
        static BinaryPakTest() => EstatePlatform.Startups.Add(TestPlatform.Startup);

        [Theory]
        [InlineData("game:/main.key#Witcher", "dialogues00.bif:09_ban2ban01.dlg")]
        public void DLG(string uri, string sampleFile) => EstateLoadFileObject<BinaryPak>("Red", uri, sampleFile);

        [Theory]
        [InlineData("game:/main.key#Witcher", "quests00.bif:act1.qdb")]
        public void QDB(string uri, string sampleFile) => EstateLoadFileObject<BinaryPak>("Red", uri, sampleFile);

        [Theory]
        [InlineData("game:/main.key#Witcher", "quests00.bif:q1000_act1_init.qst")]
        public void QST(string uri, string sampleFile) => EstateLoadFileObject<BinaryPak>("Red", uri, sampleFile);

        //[Theory]
        //[InlineData("game:/main.key#Witcher", "meshes00.bif/alpha_dummy.mdb")]
        //public void MDB(string uri, string sampleFile) => EstateLoadFileObject<BinaryPak>("Red", uri, sampleFile);

        static void EstateLoadFileObject<T>(string estate, string uri, string sampleFile)
        {
            if (sampleFile == null) return;
            var pakFile = EstateManager.GetEstate(estate).OpenPakFile(new Uri(uri));
            Assert.True(pakFile.Contains(sampleFile));
            var result = pakFile.LoadFileObjectAsync<T>(sampleFile).Result;
        }
    }
}
