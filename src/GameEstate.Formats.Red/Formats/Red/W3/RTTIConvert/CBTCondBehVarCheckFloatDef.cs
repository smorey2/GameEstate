using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTCondBehVarCheckFloatDef : IBehTreeConditionalTaskDefinition
	{
		[Ordinal(1)] [RED("behVarName")] 		public CName BehVarName { get; set;}

		[Ordinal(2)] [RED("behVarValue")] 		public CFloat BehVarValue { get; set;}

		[Ordinal(3)] [RED("compareOperation")] 		public CEnum<ECompareOp> CompareOperation { get; set;}

		public CBTCondBehVarCheckFloatDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTCondBehVarCheckFloatDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}