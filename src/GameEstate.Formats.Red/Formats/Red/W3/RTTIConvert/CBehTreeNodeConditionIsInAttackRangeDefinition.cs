using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehTreeNodeConditionIsInAttackRangeDefinition : CBehTreeNodeConditionDefinition
	{
		[Ordinal(1)] [RED("attackRangeName")] 		public CBehTreeValCName AttackRangeName { get; set;}

		[Ordinal(2)] [RED("predictPositionInTime")] 		public CBehTreeValFloat PredictPositionInTime { get; set;}

		public CBehTreeNodeConditionIsInAttackRangeDefinition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehTreeNodeConditionIsInAttackRangeDefinition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}