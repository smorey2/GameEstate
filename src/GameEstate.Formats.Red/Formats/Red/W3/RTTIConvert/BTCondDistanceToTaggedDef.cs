using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class BTCondDistanceToTaggedDef : IBehTreeConditionalTaskDefinition
	{
		[Ordinal(1)] [RED("minDistance")] 		public CBehTreeValFloat MinDistance { get; set;}

		[Ordinal(2)] [RED("maxDistance")] 		public CBehTreeValFloat MaxDistance { get; set;}

		[Ordinal(3)] [RED("targetTag")] 		public CBehTreeValCName TargetTag { get; set;}

		public BTCondDistanceToTaggedDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new BTCondDistanceToTaggedDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}