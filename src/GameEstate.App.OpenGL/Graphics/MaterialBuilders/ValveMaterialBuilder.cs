using GameEstate.Formats.Valve;
using GameEstate.Graphics.OpenGL;
using System;

namespace GameEstate.Graphics.MaterialBuilders
{
    public class ValveMaterialBuilder : AbstractMaterialBuilder
    {
        public ValveMaterialBuilder(TextureManager textureManager) : base(textureManager) { }

        public override Material BuildMaterial(object key)
        {
            switch (key)
            {
                case null: return BuildMaterial();
                case string p: return null;
                default: throw new ArgumentOutOfRangeException(nameof(key));
            }
        }

        static Material BuildMaterial() => new Material(null);

        //public Material LoadMaterial(BinaryPak resource)
        //{
        //    if (resource == null)
        //    {
        //        var errorMat = new Material(new DATAMaterial());
        //        errorMat.Textures["g_tColor"] = _texture.GetErrorTexture();
        //        errorMat.Info.ShaderName = "vrf.error";
        //        return errorMat;
        //    }

        //    var mat = new Material((DATAMaterial)resource.DATA);
        //    foreach (var textureReference in mat.Info.TextureParams)
        //    {
        //        var key = textureReference.Key;
        //        mat.Textures[key] = LoadTexture(textureReference.Value);
        //    }

        //    if (mat.Info.IntParams.ContainsKey("F_SOLID_COLOR") && mat.Info.IntParams["F_SOLID_COLOR"] == 1)
        //    {
        //        var a = mat.Info.VectorParams["g_vColorTint"];
        //        mat.Textures["g_tColor"] = GenerateColorTexture(1, 1, new[] { a.X, a.Y, a.Z, a.W });
        //    }

        //    if (!mat.Textures.ContainsKey("g_tColor"))
        //        mat.Textures["g_tColor"] = GetErrorTexture();

        //    // Since our shaders only use g_tColor, we have to find at least one texture to use here
        //    if (mat.Textures["g_tColor"] == GetErrorTexture())
        //    {
        //        var namesToTry = new[] { "g_tColor2", "g_tColor1", "g_tColorA", "g_tColorB", "g_tColorC" };
        //        foreach (var name in namesToTry)
        //            if (mat.Textures.ContainsKey(name))
        //            {
        //                mat.Textures["g_tColor"] = mat.Textures[name];
        //                break;
        //            }
        //    }

        //    // Set default values for scale and positions
        //    if (!mat.Info.VectorParams.ContainsKey("g_vTexCoordScale"))
        //        mat.Info.VectorParams["g_vTexCoordScale"] = Vector4.One;

        //    if (!mat.Info.VectorParams.ContainsKey("g_vTexCoordOffset"))
        //        mat.Info.VectorParams["g_vTexCoordOffset"] = Vector4.Zero;

        //    if (!mat.Info.VectorParams.ContainsKey("g_vColorTint"))
        //        mat.Info.VectorParams["g_vColorTint"] = Vector4.One;

        //    return mat;
        //}
    }
}