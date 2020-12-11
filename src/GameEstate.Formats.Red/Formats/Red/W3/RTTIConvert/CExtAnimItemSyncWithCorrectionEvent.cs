using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CExtAnimItemSyncWithCorrectionEvent : CExtAnimDurationEvent
	{
		[Ordinal(1)] [RED("equipSlot")] 		public CName EquipSlot { get; set;}

		[Ordinal(2)] [RED("holdSlot")] 		public CName HoldSlot { get; set;}

		[Ordinal(3)] [RED("action")] 		public CEnum<EItemLatentAction> Action { get; set;}

		[Ordinal(4)] [RED("correctionBone")] 		public CName CorrectionBone { get; set;}

		public CExtAnimItemSyncWithCorrectionEvent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CExtAnimItemSyncWithCorrectionEvent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}