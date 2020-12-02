//using GameEstate.Formats.Valve.Blocks;
//using GameEstate.Graphics.OpenGL;
//using OpenTK.Graphics.OpenGL;
//using System;
//using System.Collections.Generic;
//using System.Numerics;

//namespace GameEstate.Formats.Valve
//{
//    public class MaterialLoader
//    {
//        readonly IGLContext Context;


//        public MaterialLoader(IGLContext context) => Context = context;

//        public Material GetMaterial(string name)
//        {
//            if (Materials.ContainsKey(name))
//                return Materials[name];

//            var resource = Context.LoadFile<BinaryPak>($"{name}_c");
//            var mat = LoadMaterial(resource);
//            Materials.Add(name, mat);
//            return mat;
//        }


//        public int LoadTexture(string name)
//        {
//            var textureResource = Context.LoadFile<BinaryPak>($"{name}_c");
//            return textureResource == null ? GetErrorTexture() : LoadTexture(textureResource);
//        }

//    }
//}
