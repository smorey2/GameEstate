using GameEstate.Core;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.Valve.Blocks
{
    /// <summary>
    /// "REDI" block. ResourceEditInfoBlock_t.
    /// </summary>
    public class REDI : Block
    {
        /// <summary>
        /// This is not a real Valve enum, it's just the order they appear in.
        /// </summary>
        public enum REDIStruct
        {
            InputDependencies,
            AdditionalInputDependencies,
            ArgumentDependencies,
            SpecialDependencies,
            CustomDependencies,
            AdditionalRelatedFiles,
            ChildResourceList,
            ExtraIntData,
            ExtraFloatData,
            ExtraStringData,
            End,
        }

        public Dictionary<REDIStruct, REDIAbstract> Structs { get; private set; } = new Dictionary<REDIStruct, REDIAbstract>();

        public override void Read(BinaryReader r, BinaryPak resource)
        {
            r.BaseStream.Position = Offset;
            for (var i = REDIStruct.InputDependencies; i < REDIStruct.End; i++)
            {
                var block = REDIFactory(i);
                block.Offset = (uint)r.BaseStream.Position + r.ReadUInt32();
                block.Size = r.ReadUInt32();
                Structs.Add(i, block);
            }
            foreach (var block in Structs)
                block.Value.Read(r, resource);
        }

        public override void WriteText(IndentedTextWriter w)
        {
            w.WriteLine("ResourceEditInfoBlock_t");
            w.WriteLine("{");
            w.Indent++;
            foreach (var dep in Structs)
                dep.Value.WriteText(w);
            w.Indent--;
            w.WriteLine("}");
        }

        static REDIAbstract REDIFactory(REDIStruct id)
        {
            switch (id)
            {
                case REDIStruct.InputDependencies: return new REDIInputDependencies();
                case REDIStruct.AdditionalInputDependencies: return new REDIAdditionalInputDependencies();
                case REDIStruct.ArgumentDependencies: return new REDIArgumentDependencies();
                case REDIStruct.SpecialDependencies: return new REDISpecialDependencies();
                case REDIStruct.CustomDependencies: return new REDICustomDependencies();
                case REDIStruct.AdditionalRelatedFiles: return new REDIAdditionalRelatedFiles();
                case REDIStruct.ChildResourceList: return new REDIChildResourceList();
                case REDIStruct.ExtraIntData: return new REDIExtraIntData();
                case REDIStruct.ExtraFloatData: return new REDIExtraFloatData();
                case REDIStruct.ExtraStringData: return new REDIExtraStringData();
                default: throw new InvalidDataException("Unknown struct in REDI block.");
            }
        }
    }
}
