using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3BeehiveStandingEntity : W3AnimatedContainer
	{
		[Ordinal(1)] [RED("damageVal")] 		public SAbilityAttributeValue DamageVal { get; set;}

		[Ordinal(2)] [RED("actorsInRange", 2,0)] 		public CArray<CHandle<CActor>> ActorsInRange { get; set;}

		[Ordinal(3)] [RED("wasInteracted")] 		public CBool WasInteracted { get; set;}

		public W3BeehiveStandingEntity(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3BeehiveStandingEntity(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}