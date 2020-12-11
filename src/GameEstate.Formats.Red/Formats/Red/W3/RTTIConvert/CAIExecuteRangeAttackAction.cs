using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAIExecuteRangeAttackAction : IAIActionTree
	{
		[Ordinal(1)] [RED("attackParameter")] 		public CEnum<EAttackType> AttackParameter { get; set;}

		[Ordinal(2)] [RED("targetTag")] 		public CName TargetTag { get; set;}

		[Ordinal(3)] [RED("projectileName")] 		public CName ProjectileName { get; set;}

		public CAIExecuteRangeAttackAction(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAIExecuteRangeAttackAction(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}