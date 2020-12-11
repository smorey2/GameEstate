using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphBlendMultipleCondNode_Multi : IBehaviorGraphBlendMultipleCondNode_Condition
	{
		[Ordinal(1)] [RED("conditions", 2,0)] 		public CArray<CPtr<IBehaviorGraphBlendMultipleCondNode_Condition>> Conditions { get; set;}

		[Ordinal(2)] [RED("logicAndOr")] 		public CBool LogicAndOr { get; set;}

		public CBehaviorGraphBlendMultipleCondNode_Multi(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphBlendMultipleCondNode_Multi(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}