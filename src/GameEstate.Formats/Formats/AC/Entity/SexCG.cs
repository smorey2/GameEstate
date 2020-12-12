using GameEstate.Core;
using System;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class SexCG
    {
        public string Name;
        public uint Scale;
        public uint SetupID;
        public uint SoundTable;
        public uint IconImage;
        public uint BasePalette;
        public uint SkinPalSet;
        public uint PhysicsTable;
        public uint MotionTable;
        public uint CombatTable;
        public ObjDesc BaseObjDesc;
        public uint[] HairColorList;
        public HairStyleCG[] HairStyleList;
        public uint[] EyeColorList;
        public EyeStripCG[] EyeStripList;
        public FaceStripCG[] NoseStripList;
        public FaceStripCG[] MouthStripList;
        public GearCG[] HeadgearList;
        public GearCG[] ShirtList;
        public GearCG[] PantsList;
        public GearCG[] FootwearList;
        public uint[] ClothingColorsList;

        public SexCG(BinaryReader r)
        {
            Name = r.ReadString();
            Scale = r.ReadUInt32();
            SetupID = r.ReadUInt32();
            SoundTable = r.ReadUInt32();
            IconImage = r.ReadUInt32();
            BasePalette = r.ReadUInt32();
            SkinPalSet = r.ReadUInt32();
            PhysicsTable = r.ReadUInt32();
            MotionTable = r.ReadUInt32();
            CombatTable = r.ReadUInt32();
            BaseObjDesc = new ObjDesc(r);
            HairColorList = r.ReadC32Array<uint>(sizeof(uint));
            HairStyleList = r.ReadC32Array(x => new HairStyleCG(x));
            EyeColorList = r.ReadC32Array<uint>(sizeof(uint));
            EyeStripList = r.ReadC32Array(x => new EyeStripCG(x));
            NoseStripList = r.ReadC32Array(x => new FaceStripCG(x));
            MouthStripList = r.ReadC32Array(x => new FaceStripCG(x));
            HeadgearList = r.ReadC32Array(x => new GearCG(x));
            ShirtList = r.ReadC32Array(x => new GearCG(x));
            PantsList = r.ReadC32Array(x => new GearCG(x));
            FootwearList = r.ReadC32Array(x => new GearCG(x));
            ClothingColorsList = r.ReadC32Array<uint>(sizeof(uint));
        }

        // Eyes
        public uint GetEyeTexture(uint eyesStrip, bool isBald) => (isBald ? EyeStripList[Convert.ToInt32(eyesStrip)].ObjDescBald : EyeStripList[Convert.ToInt32(eyesStrip)].ObjDesc).TextureChanges[0].NewTexture;
        public uint GetDefaultEyeTexture(uint eyesStrip, bool isBald) => (isBald ? EyeStripList[Convert.ToInt32(eyesStrip)].ObjDescBald : EyeStripList[Convert.ToInt32(eyesStrip)].ObjDesc).TextureChanges[0].OldTexture;

        // Nose
        public uint GetNoseTexture(uint noseStrip) => NoseStripList[Convert.ToInt32(noseStrip)].ObjDesc.TextureChanges[0].NewTexture;
        public uint GetDefaultNoseTexture(uint noseStrip) => NoseStripList[Convert.ToInt32(noseStrip)].ObjDesc.TextureChanges[0].OldTexture;

        // Mouth
        public uint GetMouthTexture(uint mouthStrip) => MouthStripList[Convert.ToInt32(mouthStrip)].ObjDesc.TextureChanges[0].NewTexture;
        public uint GetDefaultMouthTexture(uint mouthStrip) => MouthStripList[Convert.ToInt32(mouthStrip)].ObjDesc.TextureChanges[0].OldTexture;

        // Hair (Head)
        public uint? GetHeadObject(uint hairStyle)
        {
            var hairstyle = HairStyleList[Convert.ToInt32(hairStyle)];
            // Gear Knights, both Olthoi types have multiple anim part changes.
            return hairstyle.ObjDesc.AnimPartChanges.Count == 1 ? hairstyle.ObjDesc.AnimPartChanges[0].PartID : (uint?)null;
        }
        public uint GetHairTexture(uint hairStyle) => HairStyleList[Convert.ToInt32(hairStyle)].ObjDesc.TextureChanges[0].NewTexture;
        public uint GetDefaultHairTexture(uint hairStyle) => HairStyleList[Convert.ToInt32(hairStyle)].ObjDesc.TextureChanges[0].OldTexture;

        // Headgear
        public uint GetHeadgearWeenie(uint headgearStyle) => HeadgearList[Convert.ToInt32(headgearStyle)].WeenieDefault;
        public uint GetHeadgearClothingTable(uint headgearStyle) => HeadgearList[Convert.ToInt32(headgearStyle)].ClothingTable;

        // Shirt
        public uint GetShirtWeenie(uint shirtStyle) => ShirtList[Convert.ToInt32(shirtStyle)].WeenieDefault;
        public uint GetShirtClothingTable(uint shirtStyle) => ShirtList[Convert.ToInt32(shirtStyle)].ClothingTable;

        // Pants
        public uint GetPantsWeenie(uint pantsStyle) => PantsList[Convert.ToInt32(pantsStyle)].WeenieDefault;
        public uint GetPantsClothingTable(uint pantsStyle) => PantsList[Convert.ToInt32(pantsStyle)].ClothingTable;

        // Footwear
        public uint GetFootwearWeenie(uint footwearStyle) => FootwearList[Convert.ToInt32(footwearStyle)].WeenieDefault;
        public uint GetFootwearClothingTable(uint footwearStyle) => FootwearList[Convert.ToInt32(footwearStyle)].ClothingTable;
    }
}
