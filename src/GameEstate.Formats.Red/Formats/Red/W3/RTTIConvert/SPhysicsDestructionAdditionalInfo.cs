using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SPhysicsDestructionAdditionalInfo : CVariable
	{
		[Ordinal(1)] [RED("initialVelocity")] 		public Vector InitialVelocity { get; set;}

		[Ordinal(2)] [RED("overrideCollisionMasks")] 		public CBool OverrideCollisionMasks { get; set;}

		[Ordinal(3)] [RED("m_collisionType")] 		public CPhysicalCollision M_collisionType { get; set;}

		public SPhysicsDestructionAdditionalInfo(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SPhysicsDestructionAdditionalInfo(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}