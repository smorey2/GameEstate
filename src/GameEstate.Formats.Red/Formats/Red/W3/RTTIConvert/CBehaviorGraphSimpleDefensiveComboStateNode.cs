using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphSimpleDefensiveComboStateNode : CBehaviorGraphComboStateNode
	{
		[Ordinal(1)] [RED("varHitTime")] 		public CName VarHitTime { get; set;}

		[Ordinal(2)] [RED("varHitLevel")] 		public CName VarHitLevel { get; set;}

		[Ordinal(3)] [RED("varElemEnum")] 		public CName VarElemEnum { get; set;}

		[Ordinal(4)] [RED("varParry")] 		public CName VarParry { get; set;}

		[Ordinal(5)] [RED("enum")] 		public CName Enum { get; set;}

		[Ordinal(6)] [RED("animElems", 2,0)] 		public CArray<SBehaviorComboElem> AnimElems { get; set;}

		public CBehaviorGraphSimpleDefensiveComboStateNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphSimpleDefensiveComboStateNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}