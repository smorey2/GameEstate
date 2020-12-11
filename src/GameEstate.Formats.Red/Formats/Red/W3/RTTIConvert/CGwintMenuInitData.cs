using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CGwintMenuInitData : CObject
	{
		[Ordinal(1)] [RED("deckName")] 		public CName DeckName { get; set;}

		[Ordinal(2)] [RED("difficulty")] 		public CEnum<EGwintDifficultyMode> Difficulty { get; set;}

		[Ordinal(3)] [RED("aggression")] 		public CEnum<EGwintAggressionMode> Aggression { get; set;}

		[Ordinal(4)] [RED("allowMultipleMatches")] 		public CBool AllowMultipleMatches { get; set;}

		[Ordinal(5)] [RED("forceFaction")] 		public CEnum<eGwintFaction> ForceFaction { get; set;}

		public CGwintMenuInitData(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CGwintMenuInitData(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}