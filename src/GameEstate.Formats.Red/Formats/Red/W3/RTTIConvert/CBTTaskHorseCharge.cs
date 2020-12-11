using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskHorseCharge : IBehTreeTask
	{
		[Ordinal(1)] [RED("dealDamage")] 		public CBool DealDamage { get; set;}

		[Ordinal(2)] [RED("collisionWithActor")] 		public CBool CollisionWithActor { get; set;}

		[Ordinal(3)] [RED("xmlDamageName")] 		public CName XmlDamageName { get; set;}

		[Ordinal(4)] [RED("collidedActor")] 		public CHandle<CActor> CollidedActor { get; set;}

		public CBTTaskHorseCharge(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskHorseCharge(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}