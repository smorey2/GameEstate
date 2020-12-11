using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CMoveSteeringCompositeCondition : IMoveSteeringCondition
	{
		[Ordinal(1)] [RED("firstCondition")] 		public CPtr<IMoveSteeringCondition> FirstCondition { get; set;}

		[Ordinal(2)] [RED("notFirstCondition")] 		public CBool NotFirstCondition { get; set;}

		[Ordinal(3)] [RED("operator")] 		public CEnum<ELogicOperator> Operator { get; set;}

		[Ordinal(4)] [RED("secondCondition")] 		public CPtr<IMoveSteeringCondition> SecondCondition { get; set;}

		[Ordinal(5)] [RED("notSecondCondition")] 		public CBool NotSecondCondition { get; set;}

		public CMoveSteeringCompositeCondition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CMoveSteeringCompositeCondition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}