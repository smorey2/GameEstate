using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SNotWorkingOutFunctionParametersHackStruct1 : CVariable
	{
		[Ordinal(1)] [RED("outValue")] 		public CInt32 OutValue { get; set;}

		[Ordinal(2)] [RED("retValue")] 		public CBool RetValue { get; set;}

		public SNotWorkingOutFunctionParametersHackStruct1(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SNotWorkingOutFunctionParametersHackStruct1(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}