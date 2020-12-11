using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public partial class CBehaviorGraphStateNode : CBehaviorGraphContainerNode
	{
		[Ordinal(1)] [RED("groups")] 		public TagList Groups { get; set;}

		[Ordinal(2)] [RED("behaviorGraphSyncInfo")] 		public SBehaviorGraphStateBehaviorGraphSyncInfo BehaviorGraphSyncInfo { get; set;}

		[Ordinal(3)] [RED("cachedStateTransitions", 2,0)] 		public CArray<CPtr<CBehaviorGraphStateTransitionNode>> CachedStateTransitions { get; set;}

		public CBehaviorGraphStateNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphStateNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}