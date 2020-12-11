using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskRiderSetMoveAlongPathWithCompanionActionOnHorse : IBehTreeTask
	{
		[Ordinal(1)] [RED("horseMoveAlongPathAction")] 		public CHandle<CAIMoveAlongPathWithCompanionAction> HorseMoveAlongPathAction { get; set;}

		[Ordinal(2)] [RED("riderData")] 		public CHandle<CAIStorageRiderData> RiderData { get; set;}

		public CBTTaskRiderSetMoveAlongPathWithCompanionActionOnHorse(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskRiderSetMoveAlongPathWithCompanionActionOnHorse(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}