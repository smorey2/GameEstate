using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3Boat : CGameplayEntity
	{
		[Ordinal(1)] [RED("teleportedFromOtherHUB")] 		public CBool TeleportedFromOtherHUB { get; set;}

		[Ordinal(2)] [RED("boatComp")] 		public CHandle<CBoatComponent> BoatComp { get; set;}

		[Ordinal(3)] [RED("mountInteractionComp")] 		public CHandle<CInteractionComponent> MountInteractionComp { get; set;}

		[Ordinal(4)] [RED("mountInteractionCompPassenger")] 		public CHandle<CInteractionComponent> MountInteractionCompPassenger { get; set;}

		[Ordinal(5)] [RED("canBeDestroyed")] 		public CBool CanBeDestroyed { get; set;}

		[Ordinal(6)] [RED("needEnableInteractions")] 		public CBool NeedEnableInteractions { get; set;}

		public W3Boat(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3Boat(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}