using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class BTCondCheckRotationToTargetDef : IBehTreeConditionalTaskDefinition
	{
		[Ordinal(1)] [RED("ifNot")] 		public CBool IfNot { get; set;}

		[Ordinal(2)] [RED("toleranceAngle")] 		public CFloat ToleranceAngle { get; set;}

		public BTCondCheckRotationToTargetDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new BTCondCheckRotationToTargetDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}