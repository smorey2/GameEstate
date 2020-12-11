using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphAnimationBaseSlotNode : CBehaviorGraphMimicsAnimationNode
	{
		[Ordinal(1)] [RED("slotName")] 		public CName SlotName { get; set;}

		[Ordinal(2)] [RED("animPrefix")] 		public CString AnimPrefix { get; set;}

		[Ordinal(3)] [RED("animSufix")] 		public CString AnimSufix { get; set;}

		public CBehaviorGraphAnimationBaseSlotNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphAnimationBaseSlotNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}