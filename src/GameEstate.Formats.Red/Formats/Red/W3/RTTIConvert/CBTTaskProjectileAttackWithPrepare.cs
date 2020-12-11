using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskProjectileAttackWithPrepare : CBTTaskProjectileAttack
	{
		[Ordinal(1)] [RED("boneName")] 		public CName BoneName { get; set;}

		[Ordinal(2)] [RED("rawTarget")] 		public CBool RawTarget { get; set;}

		[Ordinal(3)] [RED("shootInFront")] 		public CBool ShootInFront { get; set;}

		[Ordinal(4)] [RED("shootInFrontOffset")] 		public CFloat ShootInFrontOffset { get; set;}

		public CBTTaskProjectileAttackWithPrepare(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskProjectileAttackWithPrepare(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}