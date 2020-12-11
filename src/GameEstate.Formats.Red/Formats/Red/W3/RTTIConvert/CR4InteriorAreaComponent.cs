using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CR4InteriorAreaComponent : CTriggerAreaComponent
	{
		[Ordinal(1)] [RED("entranceTag")] 		public CName EntranceTag { get; set;}

		[Ordinal(2)] [RED("texture")] 		public CString Texture { get; set;}

		[Ordinal(3)] [RED("isDarkPlace")] 		public CBool IsDarkPlace { get; set;}

		[Ordinal(4)] [RED("allowHorseInThisInterior")] 		public CBool AllowHorseInThisInterior { get; set;}

		[Ordinal(5)] [RED("movementLock")] 		public CEnum<EPlayerMovementLockType> MovementLock { get; set;}

		public CR4InteriorAreaComponent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CR4InteriorAreaComponent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}