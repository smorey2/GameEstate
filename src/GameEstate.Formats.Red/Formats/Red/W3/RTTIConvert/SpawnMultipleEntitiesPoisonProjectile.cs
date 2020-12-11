using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SpawnMultipleEntitiesPoisonProjectile : PoisonProjectile
	{
		[Ordinal(1)] [RED("numberOfSpawns")] 		public CInt32 NumberOfSpawns { get; set;}

		[Ordinal(2)] [RED("minDistFromTarget")] 		public CInt32 MinDistFromTarget { get; set;}

		[Ordinal(3)] [RED("maxDistFromTarget")] 		public CInt32 MaxDistFromTarget { get; set;}

		public SpawnMultipleEntitiesPoisonProjectile(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SpawnMultipleEntitiesPoisonProjectile(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}