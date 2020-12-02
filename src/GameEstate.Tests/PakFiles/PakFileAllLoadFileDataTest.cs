using GameEstate.Core;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GameEstate.Tests.PakFiles
{
    public class PakFileAllLoadFileDataTest
    {
        static PakFileAllLoadFileDataTest() => EstatePlatform.Startups.Add(TestPlatform.Startup);

        [Theory]
        [InlineData("game:/*.dat#AC")]
        public void ACEstate(string uri, long? maxFileSize = null) => EstateLoadAll("AC", uri, maxFileSize).Wait();

        [Theory]
        [InlineData("game:/*.index#Dishonored2", 10000000)]
        public void ArkaneEstate(string uri, long? maxFileSize = null) => EstateLoadAll("Arkane", uri, maxFileSize).Wait();

        [Theory]
        [InlineData("game:/*.pak#MechWarriorOnline")]
        public void CryEstate(string uri, long? maxFileSize = null) => EstateLoadAll("Cry", uri, maxFileSize).Wait();

        [Theory]
        [InlineData("game:/*.cpk#TheCouncil")]
        public void CyanideEstate(string uri, long? maxFileSize = null) => EstateLoadAll("Cyanide", uri, maxFileSize).Wait();

        [Theory]
        //[InlineData("game:/anim.idx#UltimaOnline")]
        [InlineData("game:/static/*.flx#UltimaIX")]
        public void OriginEstate(string uri, long? maxFileSize = null) => EstateLoadAll("Origin", uri, maxFileSize).Wait();

        [Theory]
        [InlineData("game:/Data.p4k#StarCitizen", 10000000)]
        public void RsiEstate(string uri, long? maxFileSize = null) => EstateLoadAll("Rsi", uri, maxFileSize).Wait();

        [Theory]
        [InlineData("game:/main.key#Witcher")]
        [InlineData("game:/krbr.dzip#Witcher2")]
        [InlineData("game:/content0/bundles/xml.bundle#Witcher3")]
        [InlineData("game:/content0/collision.cache#Witcher3")]
        [InlineData("game:/content0/dep.cache#Witcher3")]
        public void RedEstate(string uri, long? maxFileSize = null) => EstateLoadAll("Red", uri, maxFileSize).Wait();

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
        [InlineData("game:/SeventySix*.ba2#Fallout76", 15000000)]
        public void TesEstate(string uri, long? maxFileSize = null) => EstateLoadAll("Tes", uri, maxFileSize).Wait();

        [Theory]
        [InlineData("game:/(core:dota)/*_dir.vpk#Dota2", 15000000)]
        public void ValveEstate(string uri, long? maxFileSize = null) => EstateLoadAll("Valve", uri, maxFileSize).Wait();

        static async Task EstateLoadAll(string estate, string uri, long? maxFileSize)
        {
            var pakFile = EstateManager.GetEstate(estate).OpenPakFile(new Uri(uri));
            if (pakFile is MultiPakFile multiPak)
                foreach (var p in multiPak.PakFiles)
                {
                    if (!(p is BinaryPakFile pak))
                        throw new InvalidOperationException("multiPak not a BinaryPakFile");
                    await ExportAsync(pak, maxFileSize);
                }
            else await ExportAsync(pakFile, maxFileSize);
        }

        static Task ExportAsync(EstatePakFile source, long? maxSize)
        {
            if (!(source is BinaryPakManyFile multiSource))
                throw new NotSupportedException();

            // write files
            Parallel.For(0, multiSource.Files.Count, new ParallelOptions { /*MaxDegreeOfParallelism = 1*/ }, async index =>
            {
                var file = multiSource.Files[index];

                // extract pak
                if (file.Pak != null)
                {
                    await ExportAsync(file.Pak, maxSize);
                    return;
                }

                // skip empty file
                if (file.FileSize == 0 && file.PackedSize == 0)
                    return;

                // skip large files
                if (maxSize != null && file.FileSize > maxSize.Value)
                    return;

                // extract file
                using (var s = await multiSource.LoadFileDataAsync(file))
                    s.ReadAllBytes();
            });

            return Task.CompletedTask;
        }
    }
}
