using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphSyncOverrideStateMachineNode : CBehaviorGraphSelfActivatingStateMachineNode
	{
		[Ordinal(1)] [RED("rootBoneName")] 		public CString RootBoneName { get; set;}

		[Ordinal(2)] [RED("blendRootParent")] 		public CBool BlendRootParent { get; set;}

		[Ordinal(3)] [RED("defaultWeight")] 		public CFloat DefaultWeight { get; set;}

		[Ordinal(4)] [RED("mergeEvents")] 		public CBool MergeEvents { get; set;}

		public CBehaviorGraphSyncOverrideStateMachineNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphSyncOverrideStateMachineNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}