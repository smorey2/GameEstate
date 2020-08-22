using GameEstate.Graphics;
using System;
using UnityEngine;

namespace Tests
{
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
            var materialManager = PakFile.MaterialManager;
            var obj = GameObject.CreatePrimitive(PrimitiveType.Cube); // GameObject.Find("Cube"); // CreatePrimitive(PrimitiveType.Cube);
            var meshRenderer = obj.GetComponent<MeshRenderer>();
            var materialProps = new MaterialProps
            {
                Textures = new MaterialTextures { MainFilePath = path },
            };
            meshRenderer.material = materialManager.BuildMaterialFromProperties(materialProps);
            return obj;
        }

        void MakeCursor(string path) => Cursor.SetCursor(PakFile.LoadTexture(path), Vector2.zero, CursorMode.Auto);
    }
}