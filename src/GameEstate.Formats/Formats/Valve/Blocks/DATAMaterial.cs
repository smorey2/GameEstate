using GameEstate.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace GameEstate.Formats.Valve.Blocks
{
    public class DATAMaterial : DATABinaryKV3OrNTRO, IParamMaterialInfo
    {
        public string Name { get; set; }
        public string ShaderName { get; set; }

        public Dictionary<string, long> IntParams { get; } = new Dictionary<string, long>();
        public Dictionary<string, float> FloatParams { get; } = new Dictionary<string, float>();
        public Dictionary<string, Vector4> VectorParams { get; } = new Dictionary<string, Vector4>();
        public Dictionary<string, string> TextureParams { get; } = new Dictionary<string, string>();
        public Dictionary<string, long> IntAttributes { get; } = new Dictionary<string, long>();
        public Dictionary<string, float> FloatAttributes { get; } = new Dictionary<string, float>();
        public Dictionary<string, Vector4> VectorAttributes { get; } = new Dictionary<string, Vector4>();
        public Dictionary<string, string> StringAttributes { get; } = new Dictionary<string, string>();

        public override void Read(BinaryPak parent, BinaryReader r)
        {
            base.Read(parent, r);
            Name = Data.Get<string>("m_materialName");
            ShaderName = Data.Get<string>("m_shaderName");
            // TODO: Is this a string array?
            //RenderAttributesUsed = Data.Get<string>("m_renderAttributesUsed");
            foreach (var kv in Data.GetArray("m_intParams"))
                IntParams[kv.Get<string>("m_name")] = kv.GetInt("m_nValue");
            foreach (var kv in Data.GetArray("m_floatParams"))
                FloatParams[kv.Get<string>("m_name")] = kv.GetFloat("m_flValue");
            foreach (var kv in Data.GetArray("m_vectorParams"))
                VectorParams[kv.Get<string>("m_name")] = kv.Get<Vector4>("m_value");
            foreach (var kv in Data.GetArray("m_textureParams"))
                TextureParams[kv.Get<string>("m_name")] = $"{kv.Get<string>("m_pValue")}_c";
            // TODO: These 3 parameters
            //var textureAttributes = Data.GetArray("m_textureAttributes");
            //var dynamicParams = Data.GetArray("m_dynamicParams");
            //var dynamicTextureParams = Data.GetArray("m_dynamicTextureParams");
            foreach (var kv in Data.GetArray("m_intAttributes"))
                IntAttributes[kv.Get<string>("m_name")] = kv.GetInt("m_nValue");
            foreach (var kv in Data.GetArray("m_floatAttributes"))
                FloatAttributes[kv.Get<string>("m_name")] = kv.GetFloat("m_flValue");
            foreach (var kv in Data.GetArray("m_vectorAttributes"))
                VectorAttributes[kv.Get<string>("m_name")] = kv.Get<Vector4>("m_value");
            foreach (var kv in Data.GetArray("m_stringAttributes"))
                StringAttributes[kv.Get<string>("m_name")] = kv.Get<string>("m_pValue");
        }

        public IDictionary<string, bool> GetShaderArgs()
        {
            var arguments = new Dictionary<string, bool>();
            if (Data == null)
                return arguments;
            foreach (var intParam in Data.GetArray("m_intParams"))
            {
                var name = intParam.Get<string>("m_name");
                var value = intParam.GetInt("m_nValue");
                arguments.Add(name, value != 0);
            }
            var specialDeps = (REDISpecialDependencies)Parent.REDI.Structs[REDI.REDIStruct.SpecialDependencies];
            var hemiOctIsoRoughness_RG_B = specialDeps.List.Any(dependancy => dependancy.CompilerIdentifier == "CompileTexture" && dependancy.String == "Texture Compiler Version Mip HemiOctIsoRoughness_RG_B");
            var invert = specialDeps.List.Any(dependancy => dependancy.CompilerIdentifier == "CompileTexture" && dependancy.String == "Texture Compiler Version LegacySource1InvertNormals");
            if (hemiOctIsoRoughness_RG_B)
                arguments.Add("HemiOctIsoRoughness_RG_B", true);
            if (invert)
                arguments.Add("LegacySource1InvertNormals", true);
            return arguments;
        }
    }
}
