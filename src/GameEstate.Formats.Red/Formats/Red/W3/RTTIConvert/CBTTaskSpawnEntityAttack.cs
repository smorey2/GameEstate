using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskSpawnEntityAttack : CBTTaskAttack
	{
		[Ordinal(1)] [RED("offsetVector")] 		public Vector OffsetVector { get; set;}

		[Ordinal(2)] [RED("ressourceName")] 		public CName RessourceName { get; set;}

		[Ordinal(3)] [RED("entityTemplate")] 		public CHandle<CEntityTemplate> EntityTemplate { get; set;}

		[Ordinal(4)] [RED("spawnAnimEventName")] 		public CName SpawnAnimEventName { get; set;}

		[Ordinal(5)] [RED("completeAfterSpawn")] 		public CBool CompleteAfterSpawn { get; set;}

		[Ordinal(6)] [RED("m_summonerComponent")] 		public CHandle<W3SummonerComponent> M_summonerComponent { get; set;}

		[Ordinal(7)] [RED("couldntLoadResource")] 		public CBool CouldntLoadResource { get; set;}

		public CBTTaskSpawnEntityAttack(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskSpawnEntityAttack(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}