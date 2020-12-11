using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskGuardDef : IBehTreeTaskDefinition
	{
		[Ordinal(1)] [RED("guardArea_var")] 		public CName GuardArea_var { get; set;}

		[Ordinal(2)] [RED("guardPursuitArea_var")] 		public CName GuardPursuitArea_var { get; set;}

		[Ordinal(3)] [RED("guardPursuitRange")] 		public CBehTreeValFloat GuardPursuitRange { get; set;}

		[Ordinal(4)] [RED("guardRetreatType")] 		public CHandle<CBTEnumMoveType> GuardRetreatType { get; set;}

		[Ordinal(5)] [RED("guardRetreatSpeed")] 		public CBehTreeValFloat GuardRetreatSpeed { get; set;}

		[Ordinal(6)] [RED("guardIntruderTestFrequency")] 		public CBehTreeValFloat GuardIntruderTestFrequency { get; set;}

		public CBTTaskGuardDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskGuardDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}