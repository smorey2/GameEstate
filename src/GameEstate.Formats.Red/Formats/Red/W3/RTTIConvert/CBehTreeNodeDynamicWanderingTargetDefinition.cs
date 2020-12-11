using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehTreeNodeDynamicWanderingTargetDefinition : CBehTreeNodeSelectWanderingTargetDecoratorDefinition
	{
		[Ordinal(1)] [RED("dynamicWanderAreaName_var")] 		public CName DynamicWanderAreaName_var { get; set;}

		[Ordinal(2)] [RED("minimalWanderDistance")] 		public CBehTreeValFloat MinimalWanderDistance { get; set;}

		[Ordinal(3)] [RED("useGuardArea")] 		public CBehTreeValBool UseGuardArea { get; set;}

		public CBehTreeNodeDynamicWanderingTargetDefinition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehTreeNodeDynamicWanderingTargetDefinition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}