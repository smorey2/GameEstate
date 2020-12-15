using GameEstate.Formats.AC.FileTypes;
using GameEstate.Formats.Valve;
using System;
using Xunit;
using Environment = GameEstate.Formats.AC.FileTypes.Environment;

namespace GameEstate.Tests.Formats.AC
{
    public class BinaryPakTest
    {
        static BinaryPakTest() => EstatePlatform.Startups.Add(TestPlatform.Startup);

        [Theory]
        [InlineData("game:/client_portal.dat#AC", "0E000002")]
        [InlineData("game:/client_portal.dat#AC", "0E000003")]
        [InlineData("game:/client_portal.dat#AC", "0E000004")]
        [InlineData("game:/client_portal.dat#AC", "0E00000E")]
        [InlineData("game:/client_portal.dat#AC", "0E00000F")]
        [InlineData("game:/client_portal.dat#AC", "0E000018")]
        [InlineData("game:/client_portal.dat#AC", "0E00001D")]
        [InlineData("game:/client_portal.dat#AC", "0E001001")]
        [InlineData("game:/client_portal.dat#AC", "0E001002")]
        [InlineData("game:/client_portal.dat#AC", "30000000")]
        [InlineData("game:/client_portal.dat#AC", "3000004D")]
        [InlineData("game:/client_portal.dat#AC", "FFFF0001")]
        public void Unknown(string uri, string sampleFile) => EstateLoadFileObject<object>("AC", uri, sampleFile);

        [Theory]
        [InlineData("game:/client_cell_1.dat#AC", "0000FFFF.land")]
        public void LandBlock(string uri, string sampleFile) => EstateLoadFileObject<Landblock>("AC", uri, sampleFile);

        [Theory]
        [InlineData("game:/client_cell_1.dat#AC", "00E1FFFE.lbi")]
        public void LandBlockInfo(string uri, string sampleFile) => EstateLoadFileObject<LandblockInfo>("AC", uri, sampleFile);

        [Theory]
        [InlineData("game:/client_cell_1.dat#AC", "00010100.cell")]
        public void EnvCell(string uri, string sampleFile) => EstateLoadFileObject<EnvCell>("AC", uri, sampleFile);

        [Theory]
        [InlineData("game:/client_portal.dat#AC", "01000001.obj")]
        [InlineData("game:/client_portal.dat#AC", "01004E59.obj")]
        public void GraphicsObject(string uri, string sampleFile) => EstateLoadFileObject<GfxObj>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "02000001.set")]
        [InlineData("game:/client_portal.dat#AC", "02001C56.set")]
        public void Setup(string uri, string sampleFile) => EstateLoadFileObject<SetupModel>("AC", uri, sampleFile);
        
        [InlineData("game:/client_portal.dat#AC", "03000001.anm")]
        [InlineData("game:/client_portal.dat#AC", "03000E24.anm")]
        public void Animation(string uri, string sampleFile) => EstateLoadFileObject<Animation>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "0400007E.pal")]
        [InlineData("game:/client_portal.dat#AC", "04002059.pal")]
        public void Palette(string uri, string sampleFile) => EstateLoadFileObject<Palette>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "0500000C.texture")]
        [InlineData("game:/client_portal.dat#AC", "05003358.texture")]
        public void SurfaceTexture(string uri, string sampleFile) => EstateLoadFileObject<SurfaceTexture>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "06000133.jpg")]
        [InlineData("game:/client_portal.dat#AC", "06007576.jpg")]
        public void Texture(string uri, string sampleFile) => EstateLoadFileObject<Texture>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "08000000.surface")]
        [InlineData("game:/client_portal.dat#AC", "0800194D.surface")]
        public void Surface(string uri, string sampleFile) => EstateLoadFileObject<Surface>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "09000001.dsc")]
        [InlineData("game:/client_portal.dat#AC", "09000231.dsc")]
        public void MotionTable(string uri, string sampleFile) => EstateLoadFileObject<MotionTable>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "0A000002.wav")]
        [InlineData("game:/client_portal.dat#AC", "0A0005B5.wav")]
        public void Wave(string uri, string sampleFile) => EstateLoadFileObject<Wave>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "0D000002.env")]
        [InlineData("game:/client_portal.dat#AC", "0D00063F.env")]
        public void Environment(string uri, string sampleFile) => EstateLoadFileObject<Environment>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "0E000007.cps")]
        public void ChatPoseTable(string uri, string sampleFile) => EstateLoadFileObject<ChatPoseTable>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "0E00000D.hrc")]
        public void ObjectHierarchy(string uri, string sampleFile) => EstateLoadFileObject<GeneratorTable>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "0E00001A.bad")]
        public void BadData(string uri, string sampleFile) => EstateLoadFileObject<BadData>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "0E00001E.taboo")]
        public void TabooTable(string uri, string sampleFile) => EstateLoadFileObject<TabooTable>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "0E00001F")] //??
        public void FileToId(string uri, string sampleFile) => EstateLoadFileObject<object>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "0E000020.nft")]
        public void NameFilterTable(string uri, string sampleFile) => EstateLoadFileObject<NameFilterTable>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "0E020000.monprop")] //? where?
        public void MonitoredProperties(string uri, string sampleFile) => EstateLoadFileObject<object>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "0F000001.pst")]
        [InlineData("game:/client_portal.dat#AC", "0F000B6B.pst")]
        public void PaletteSet(string uri, string sampleFile) => EstateLoadFileObject<PaletteSet>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "10000001.clo")]
        [InlineData("game:/client_portal.dat#AC", "1000086C.clo")]
        public void Clothing(string uri, string sampleFile) => EstateLoadFileObject<ClothingTable>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "11000000.deg")]
        [InlineData("game:/client_portal.dat#AC", "110010BF.deg")]
        public void DegradeInfo(string uri, string sampleFile) => EstateLoadFileObject<GfxObjDegradeInfo>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "12000009.scn")]
        [InlineData("game:/client_portal.dat#AC", "120002C6.scn")]
        public void Scene(string uri, string sampleFile) => EstateLoadFileObject<Scene>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "13000000.rgn")]
        public void Region(string uri, string sampleFile) => EstateLoadFileObject<RegionDesc>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "14000000.keymap")]
        [InlineData("game:/client_portal.dat#AC", "14000002.keymap")]
        public void KeyMap(string uri, string sampleFile) => EstateLoadFileObject<object>("AC", uri, sampleFile);
        
        [InlineData("game:/client_portal.dat#AC", "15000000.rtexture")]
        [InlineData("game:/client_portal.dat#AC", "15000001.rtexture")]
        public void RenderTexture(string uri, string sampleFile) => EstateLoadFileObject<RenderTexture>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "16000000.mat")]
        public void RenderMaterial(string uri, string sampleFile) => EstateLoadFileObject<object>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "17000000.mm")]
        public void MaterialModifier(string uri, string sampleFile) => EstateLoadFileObject<object>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "18000000.mi")]
        public void MaterialInstance(string uri, string sampleFile) => EstateLoadFileObject<object>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "20000001.stb")]
        [InlineData("game:/client_portal.dat#AC", "200000DA.stb")]
        public void SoundTable(string uri, string sampleFile) => EstateLoadFileObject<SoundTable>("AC", uri, sampleFile);

        [InlineData("game:/client_local_English.dat#AC", "21000000.uil")]
        [InlineData("game:/client_local_English.dat#AC", "21000075.uil")]
        public void UILayout(string uri, string sampleFile) => EstateLoadFileObject<object>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "22000005.emp")] //? where?
        public void EnumMapper(string uri, string sampleFile) => EstateLoadFileObject<EnumMapper>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "23000005.stt")]
        [InlineData("game:/client_portal.dat#AC", "2300004A.stt")]
        [InlineData("game:/client_local_English.dat#AC", "23000001.stt")]
        [InlineData("game:/client_local_English.dat#AC", "23000010.stt")]
        public void StringTable(string uri, string sampleFile) => EstateLoadFileObject<StringTable>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "25000000.imp")]
        [InlineData("game:/client_portal.dat#AC", "25000015.imp")]
        public void DidMapper(string uri, string sampleFile) => EstateLoadFileObject<DidMapper>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "26000000.actionmap")]
        public void ActionMap(string uri, string sampleFile) => EstateLoadFileObject<object>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "27000000.dimp")]
        [InlineData("game:/client_portal.dat#AC", "27000004.dimp")]
        public void DualDidMapper(string uri, string sampleFile) => EstateLoadFileObject<DualDidMapper>("AC", uri, sampleFile);
        
        [InlineData("game:/client_portal.dat#AC", "31000001.str")]
        [InlineData("game:/client_portal.dat#AC", "31000025.str")]
        public void String(string uri, string sampleFile) => EstateLoadFileObject<LanguageString>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "32000A83.emt")]
        public void ParticleEmitter(string uri, string sampleFile) => EstateLoadFileObject<ParticleEmitterInfo>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "33000007.pes")]
        [InlineData("game:/client_portal.dat#AC", "3300139F.pes")]
        public void PhysicsScript(string uri, string sampleFile) => EstateLoadFileObject<PhysicsScript>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "34000004.pet")]
        [InlineData("game:/client_portal.dat#AC", "340000D7.pet")]
        public void PhysicsScriptTable(string uri, string sampleFile) => EstateLoadFileObject<PhysicsScriptTable>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "39000001.mpr")]
        public void MasterProperty(string uri, string sampleFile) => EstateLoadFileObject<object>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "40000000.font")]
        [InlineData("game:/client_portal.dat#AC", "40000032.font")]
        public void Font(string uri, string sampleFile) => EstateLoadFileObject<Font>("AC", uri, sampleFile);

        [InlineData("game:/client_local_English.dat#AC", "41000000.sti")]
        public void StringState(string uri, string sampleFile) => EstateLoadFileObject<LanguageInfo>("AC", uri, sampleFile);

        [InlineData("game:/client_portal.dat#AC", "78000000.dbpc")]
        [InlineData("game:/client_portal.dat#AC", "78000001.dbpc")]
        public void DbProperties(string uri, string sampleFile) => EstateLoadFileObject<object>("AC", uri, sampleFile);

        static void EstateLoadFileObject<T>(string estate, string uri, string sampleFile)
        {
            if (sampleFile == null) return;
            var pakFile = EstateManager.GetEstate(estate).OpenPakFile(new Uri(uri));
            Assert.True(pakFile.Contains(sampleFile));
            var result = pakFile.LoadFileObjectAsync<T>(sampleFile).Result;
        }
    }
}
