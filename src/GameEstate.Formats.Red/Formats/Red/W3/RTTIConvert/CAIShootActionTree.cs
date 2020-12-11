using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAIShootActionTree : IAICustomActionTree
	{
		[Ordinal(1)] [RED("targetTag")] 		public CName TargetTag { get; set;}

		[Ordinal(2)] [RED("multipleTargetsTags")] 		public CHandle<W3BehTreeValNameArray> MultipleTargetsTags { get; set;}

		[Ordinal(3)] [RED("numberOfActions")] 		public CInt32 NumberOfActions { get; set;}

		[Ordinal(4)] [RED("setProjectileOnFire")] 		public CBool SetProjectileOnFire { get; set;}

		[Ordinal(5)] [RED("afterActionIdleDuration")] 		public CFloat AfterActionIdleDuration { get; set;}

		[Ordinal(6)] [RED("afterActionIdleDurationChance")] 		public CFloat AfterActionIdleDurationChance { get; set;}

		[Ordinal(7)] [RED("useRayCastBeforeShooting")] 		public CBool UseRayCastBeforeShooting { get; set;}

		public CAIShootActionTree(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAIShootActionTree(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}