using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SBehaviorGraphStateBehaviorGraphSyncInfo : CVariable
	{
		[Ordinal(1)] [RED("Outbound sync tags", 2,0)] 		public CArray<CName> Outbound_sync_tags { get; set;}

		[Ordinal(2)] [RED("Inbound sync tags", 2,0)] 		public CArray<CName> Inbound_sync_tags { get; set;}

		[Ordinal(3)] [RED("Inbound sync priority")] 		public CInt32 Inbound_sync_priority { get; set;}

		[Ordinal(4)] [RED("All inbound tags required?")] 		public CBool All_inbound_tags_required_ { get; set;}

		public SBehaviorGraphStateBehaviorGraphSyncInfo(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SBehaviorGraphStateBehaviorGraphSyncInfo(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}