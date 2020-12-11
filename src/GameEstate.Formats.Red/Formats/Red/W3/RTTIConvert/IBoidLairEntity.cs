using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class IBoidLairEntity : CGameplayEntity
	{
		[Ordinal(1)] [RED("boidSpeciesName")] 		public CName BoidSpeciesName { get; set;}

		[Ordinal(2)] [RED("spawnFrequency")] 		public CFloat SpawnFrequency { get; set;}

		[Ordinal(3)] [RED("spawnLimit")] 		public CInt32 SpawnLimit { get; set;}

		[Ordinal(4)] [RED("totalLifetimeSpawnLimit")] 		public CInt32 TotalLifetimeSpawnLimit { get; set;}

		[Ordinal(5)] [RED("lairBoundings")] 		public EntityHandle LairBoundings { get; set;}

		[Ordinal(6)] [RED("range")] 		public CFloat Range { get; set;}

		[Ordinal(7)] [RED("visibilityRange")] 		public CFloat VisibilityRange { get; set;}

		public IBoidLairEntity(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new IBoidLairEntity(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}