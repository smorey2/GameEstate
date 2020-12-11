using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphStateTransitionGlobalBlendStreamingNode : CBehaviorGraphStateTransitionGlobalBlendNode
	{
		[Ordinal(1)] [RED("defaultStateName")] 		public CString DefaultStateName { get; set;}

		[Ordinal(2)] [RED("defaultState")] 		public CPtr<CBehaviorGraphStateNode> DefaultState { get; set;}

		public CBehaviorGraphStateTransitionGlobalBlendStreamingNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphStateTransitionGlobalBlendStreamingNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}