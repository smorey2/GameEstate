using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskDettlaffDash : CBTTaskAttack
	{
		[Ordinal(1)] [RED("OpenForAard")] 		public CBool OpenForAard { get; set;}

		[Ordinal(2)] [RED("action")] 		public CHandle<W3DamageAction> Action { get; set;}

		[Ordinal(3)] [RED("shouldCheckVisibility")] 		public CBool ShouldCheckVisibility { get; set;}

		[Ordinal(4)] [RED("shouldSignalGameplayEvent")] 		public CBool ShouldSignalGameplayEvent { get; set;}

		[Ordinal(5)] [RED("actor")] 		public CHandle<CActor> Actor { get; set;}

		public CBTTaskDettlaffDash(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskDettlaffDash(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}