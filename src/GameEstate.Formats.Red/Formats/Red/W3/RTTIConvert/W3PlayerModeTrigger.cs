using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3PlayerModeTrigger : CGameplayEntity
	{
		[Ordinal(1)] [RED("isEnabled")] 		public CBool IsEnabled { get; set;}

		[Ordinal(2)] [RED("isActive")] 		public CBool IsActive { get; set;}

		[Ordinal(3)] [RED("isPlayerInside")] 		public CBool IsPlayerInside { get; set;}

		[Ordinal(4)] [RED("playerMode")] 		public CEnum<EPlayerMode> PlayerMode { get; set;}

		public W3PlayerModeTrigger(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3PlayerModeTrigger(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}