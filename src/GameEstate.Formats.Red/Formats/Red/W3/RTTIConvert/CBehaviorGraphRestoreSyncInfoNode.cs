using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphRestoreSyncInfoNode : CBehaviorGraphBaseNode
	{
		[Ordinal(1)] [RED("storeName")] 		public CName StoreName { get; set;}

		[Ordinal(2)] [RED("syncMethod")] 		public CPtr<IBehaviorSyncMethod> SyncMethod { get; set;}

		[Ordinal(3)] [RED("restoreOnActivation")] 		public CBool RestoreOnActivation { get; set;}

		[Ordinal(4)] [RED("restoreEveryFrame")] 		public CBool RestoreEveryFrame { get; set;}

		[Ordinal(5)] [RED("restoreOnEvent")] 		public CName RestoreOnEvent { get; set;}

		public CBehaviorGraphRestoreSyncInfoNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphRestoreSyncInfoNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}