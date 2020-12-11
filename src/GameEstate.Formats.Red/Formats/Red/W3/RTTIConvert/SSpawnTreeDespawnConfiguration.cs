using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SSpawnTreeDespawnConfiguration : CVariable
	{
		[Ordinal(1)] [RED("canDespawnOnSight")] 		public CBool CanDespawnOnSight { get; set;}

		[Ordinal(2)] [RED("minDespawnRange")] 		public CFloat MinDespawnRange { get; set;}

		[Ordinal(3)] [RED("forceDespawnRange")] 		public CFloat ForceDespawnRange { get; set;}

		[Ordinal(4)] [RED("despawnDelayMin")] 		public CFloat DespawnDelayMin { get; set;}

		[Ordinal(5)] [RED("despawnDelayMax")] 		public CFloat DespawnDelayMax { get; set;}

		[Ordinal(6)] [RED("despawnTime")] 		public CFloat DespawnTime { get; set;}

		public SSpawnTreeDespawnConfiguration(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SSpawnTreeDespawnConfiguration(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}