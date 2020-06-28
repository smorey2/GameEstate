using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace GameEstate.Toy.Models
{
    public class Material : Dictionary<string, object>
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

        //public IDictionary<string, bool> GetShaderArguments()
        //{
        //    var arguments = new Dictionary<string, bool>();

        //    if (Data == null)
        //        return arguments;

        //    foreach (var intParam in Data.GetArray("m_intParams"))
        //    {
        //        var name = intParam.GetProperty<string>("m_name");
        //        var value = intParam.GetIntegerProperty("m_nValue");

        //        arguments.Add(name, value != 0);
        //    }

        //    var specialDeps = (SpecialDependencies)Resource.EditInfo.Structs[ResourceEditInfo.REDIStruct.SpecialDependencies];
        //    bool hemiOctIsoRoughness_RG_B = specialDeps.List.Any(dependancy => dependancy.CompilerIdentifier == "CompileTexture" && dependancy.String == "Texture Compiler Version Mip HemiOctIsoRoughness_RG_B");
        //    bool invert = specialDeps.List.Any(dependancy => dependancy.CompilerIdentifier == "CompileTexture" && dependancy.String == "Texture Compiler Version LegacySource1InvertNormals");
        //    if (hemiOctIsoRoughness_RG_B)
        //        arguments.Add("HemiOctIsoRoughness_RG_B", true);

        //    if (invert)
        //        arguments.Add("LegacySource1InvertNormals", true);

        //    return arguments;
        //}
    }
}
