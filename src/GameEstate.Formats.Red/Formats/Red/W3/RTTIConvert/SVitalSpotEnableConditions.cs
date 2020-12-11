using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SVitalSpotEnableConditions : CVariable
	{
		[Ordinal(1)] [RED("animationEventName")] 		public CName AnimationEventName { get; set;}

		[Ordinal(2)] [RED("VSActivatorBehTreeNodeName")] 		public CName VSActivatorBehTreeNodeName { get; set;}

		[Ordinal(3)] [RED("enableByDefault")] 		public CBool EnableByDefault { get; set;}

		public SVitalSpotEnableConditions(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SVitalSpotEnableConditions(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}