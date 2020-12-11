using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3QuestCond_ActorRotationToNode : CQCActorScriptedCondition
	{
		[Ordinal(1)] [RED("condition")] 		public CEnum<ECompareOp> Condition { get; set;}

		[Ordinal(2)] [RED("degrees")] 		public CFloat Degrees { get; set;}

		[Ordinal(3)] [RED("targetTag")] 		public CName TargetTag { get; set;}

		[Ordinal(4)] [RED("targetNode")] 		public CHandle<CNode> TargetNode { get; set;}

		[Ordinal(5)] [RED("listener")] 		public CHandle<W3QuestCond_ActorRotationToNode_Listener> Listener { get; set;}

		public W3QuestCond_ActorRotationToNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3QuestCond_ActorRotationToNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}