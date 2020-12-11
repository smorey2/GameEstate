using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskLeshyBirdAttackDef : CBTTaskAttackDef
	{
		[Ordinal(1)] [RED("loopTime")] 		public CFloat LoopTime { get; set;}

		[Ordinal(2)] [RED("attackRange")] 		public CFloat AttackRange { get; set;}

		[Ordinal(3)] [RED("projEntity")] 		public CHandle<CEntityTemplate> ProjEntity { get; set;}

		[Ordinal(4)] [RED("raiseEventName")] 		public CName RaiseEventName { get; set;}

		[Ordinal(5)] [RED("dodgeable")] 		public CBool Dodgeable { get; set;}

		public CBTTaskLeshyBirdAttackDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskLeshyBirdAttackDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}