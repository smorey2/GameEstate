using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskMaintainSpeedDef : IBehTreeTaskDefinition
	{
		[Ordinal(1)] [RED("moveType")] 		public CEnum<EMoveType> MoveType { get; set;}

		[Ordinal(2)] [RED("manageFlySpeed")] 		public CBool ManageFlySpeed { get; set;}

		[Ordinal(3)] [RED("onActivate")] 		public CBool OnActivate { get; set;}

		[Ordinal(4)] [RED("onDeactivate")] 		public CBool OnDeactivate { get; set;}

		[Ordinal(5)] [RED("speedDecay")] 		public CBool SpeedDecay { get; set;}

		[Ordinal(6)] [RED("speedDecayOnDeactivate")] 		public CBool SpeedDecayOnDeactivate { get; set;}

		[Ordinal(7)] [RED("overrideForThisTask")] 		public CBool OverrideForThisTask { get; set;}

		[Ordinal(8)] [RED("decayAfter")] 		public CFloat DecayAfter { get; set;}

		public CBTTaskMaintainSpeedDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskMaintainSpeedDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}