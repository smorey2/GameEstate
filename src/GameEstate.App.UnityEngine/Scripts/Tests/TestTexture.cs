using GameEstate.Graphics;
using UnityEngine;

namespace Tests
{
    // game:/Morrowind.bsa#Morrowind
    // http://192.168.1.3/ASSETS/Morrowind/Morrowind.bsa#Morrowind
    // game:/Skyrim*#SkyrimVR
    // game:/Fallout4*#Fallout4VR
    public class TestTexture : AbstractTest
    {
        public TestTexture(UnityTest test) : base(test) { }

        public override void Start()
        {
            if (!string.IsNullOrEmpty(Test.Param1))
                MakeTexture(Test.Param1);
            if (!string.IsNullOrEmpty(Test.Param2))
                MakeCursor(Test.Param2);
        }

        GameObject MakeTexture(string path)
        {
            var materialManager = Graphic.MaterialManager;
            var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var materialProps = new MaterialProps { Textures = new MaterialTextures { MainFilePath = path } };
            var meshRenderer = obj.GetComponent<MeshRenderer>();
            meshRenderer.material = Graphic.MaterialManager.BuildMaterialFromProperties(materialProps);
            return obj;
        }

        void MakeCursor(string path) => Cursor.SetCursor(Graphic.LoadTexture(path), Vector2.zero, CursorMode.Auto);
    }
}