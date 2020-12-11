using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3LadderInteraction : CGameplayEntity
	{
		[Ordinal(1)] [RED("associatedDoorTag")] 		public CName AssociatedDoorTag { get; set;}

		[Ordinal(2)] [RED("associatedDoor")] 		public CHandle<W3NewDoor> AssociatedDoor { get; set;}

		public W3LadderInteraction(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3LadderInteraction(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}