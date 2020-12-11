using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3BuffImmunityEntity : CGameplayEntity
	{
		[Ordinal(1)] [RED("immunities", 2,0)] 		public CArray<CEnum<EEffectType>> Immunities { get; set;}

		[Ordinal(2)] [RED("range")] 		public CFloat Range { get; set;}

		[Ordinal(3)] [RED("isActive")] 		public CBool IsActive { get; set;}

		[Ordinal(4)] [RED("actorsInRange", 2,0)] 		public CArray<CHandle<CActor>> ActorsInRange { get; set;}

		public W3BuffImmunityEntity(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3BuffImmunityEntity(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}