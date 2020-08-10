using GameEstate.Core;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GameEstate.Tests.PakFiles
{
    public class PakFileLoadAllTest
    {
        [Theory]
        [InlineData("game:/*.dat#AC")]
        public void ACEstate(string uri) => EstateLoadAll("AC", uri).Wait();

        [Theory]
        [InlineData("game:/*.index#Dishonored2")]
        public void ArkaneEstate(string uri) => EstateLoadAll("Arkane", uri).Wait();

        [Theory]
        [InlineData("game:/*.pak#MechWarriorOnline")]
        public void CryEstate(string uri) => EstateLoadAll("Cry", uri).Wait();

        [Theory]
        [InlineData("game:/*.cpk#TheCouncil")]
        public void CyanideEstate(string uri) => EstateLoadAll("Cyanide", uri).Wait();

        [Theory]
        [InlineData("game:/anim.idx#UltimaOnline")]
        [InlineData("game:/static/activity.flx#UltimaIX")]
        public void OriginEstate(string uri) => EstateLoadAll("Origin", uri).Wait();

        [Theory]
        [InlineData("game:/Data.p4k#StarCitizen")]
        public void RsiEstate(string uri) => EstateLoadAll("Rsi", uri).Wait();

        [Theory]
        [InlineData("game:/main.key#Witcher")]
        [InlineData("game:/krbr.dzip#Witcher2")]
        [InlineData("game:/content0/bundles/xml.bundle#Witcher3")]
        [InlineData("game:/content0/collision.cache#Witcher3")]
        [InlineData("game:/content0/dep.cache#Witcher3")]
        public void RedEstate(string uri) => EstateLoadAll("Red", uri).Wait();

        [Theory]
        [InlineData("game:/Morrowind.bsa#Morrowind")]
        [InlineData("game:/Oblivion*.bsa#Oblivion")]
        [InlineData("game:/Skyrim*.bsa#Skyrim")]
        [InlineData("game:/Skyrim*.bsa#SkyrimSE")]
        [InlineData("game:/*.dat#Fallout2")]
        [InlineData("game:/Fallout*.bsa#Fallout3")]
        [InlineData("game:/Fallout*.bsa#FalloutNV")]
        [InlineData("game:/Fallout4*.ba2#Fallout4")]
        [InlineData("game:/Fallout4*.ba2#Fallout4VR")]
        [InlineData("game:/SeventySix*.ba2#Fallout76")]
        public void TesEstate(string uri) => EstateLoadAll("Tes", uri).Wait();

        [Theory]
        [InlineData("game:/core/*_dir.vpk#Dota2")]
        public void ValveEstate(string uri) => EstateLoadAll("Valve", uri).Wait();

        static async Task EstateLoadAll(string estate, string uri)
        {
            var pakFile = EstateManager.GetEstate(estate).OpenPakFile(new Uri(uri));
            if (pakFile is MultiPakFile multiPak)
                foreach (var p in multiPak.PakFiles)
                {
                    if (!(p is BinaryPakFile pak))
                        throw new InvalidOperationException("multiPak not a BinaryPakFile");
                    await ExportAsync(pak);
                }
            else await ExportAsync(pakFile);
        }

        static Task ExportAsync(AbstractPakFile source)
        {
            if (!(source is BinaryPakMultiFile multiSource))
                throw new NotSupportedException();

            // write files
            Parallel.For(0, multiSource.Files.Count, new ParallelOptions { /*MaxDegreeOfParallelism = 1*/ }, async index =>
            {
                var file = multiSource.Files[index];

                // extract pak
                if (file.Pak != null)
                    await ExportAsync(file.Pak);

                // skip empty file
                if (file.FileSize == 0 && file.PackedSize == 0)
                    return;

                //// skip large files
                //if (file.FileSize > 50000000)
                //    return;

                // extract file
                using (var s = await multiSource.LoadFileDataAsync(file))
                    s.ReadAllBytes();
            });

            return Task.CompletedTask;
        }
    }
}
