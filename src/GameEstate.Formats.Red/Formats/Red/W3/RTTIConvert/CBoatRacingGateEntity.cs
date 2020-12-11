using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBoatRacingGateEntity : CGameplayEntity
	{
		[Ordinal(1)] [RED("nextGate")] 		public EntityHandle NextGate { get; set;}

		[Ordinal(2)] [RED("factOnReaching")] 		public CString FactOnReaching { get; set;}

		[Ordinal(3)] [RED("nextGateEntity")] 		public CHandle<CBoatRacingGateEntity> NextGateEntity { get; set;}

		[Ordinal(4)] [RED("isActive")] 		public CBool IsActive { get; set;}

		[Ordinal(5)] [RED("isReached")] 		public CBool IsReached { get; set;}

		public CBoatRacingGateEntity(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBoatRacingGateEntity(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}