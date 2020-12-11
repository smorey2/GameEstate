using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CSStoryPhaseSpawnTimetableEntry : CVariable
	{
		[Ordinal(1)] [RED("time")] 		public GameTime Time { get; set;}

		[Ordinal(2)] [RED("quantity")] 		public CInt32 Quantity { get; set;}

		[Ordinal(3)] [RED("respawnDelay")] 		public GameTime RespawnDelay { get; set;}

		[Ordinal(4)] [RED("respawn")] 		public CBool Respawn { get; set;}

		public CSStoryPhaseSpawnTimetableEntry(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CSStoryPhaseSpawnTimetableEntry(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}