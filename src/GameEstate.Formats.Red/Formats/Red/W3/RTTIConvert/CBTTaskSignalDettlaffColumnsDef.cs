using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskSignalDettlaffColumnsDef : IBehTreeTaskDefinition
	{
		[Ordinal(1)] [RED("npc")] 		public CHandle<CNewNPC> Npc { get; set;}

		[Ordinal(2)] [RED("summonerComponent")] 		public CHandle<W3SummonerComponent> SummonerComponent { get; set;}

		[Ordinal(3)] [RED("summonsArray", 2,0)] 		public CArray<CHandle<CEntity>> SummonsArray { get; set;}

		[Ordinal(4)] [RED("columnEntity")] 		public CHandle<CDettlaffColumn> ColumnEntity { get; set;}

		[Ordinal(5)] [RED("shouldComplete")] 		public CBool ShouldComplete { get; set;}

		[Ordinal(6)] [RED("startPumping")] 		public CBool StartPumping { get; set;}

		[Ordinal(7)] [RED("stopPumping")] 		public CBool StopPumping { get; set;}

		public CBTTaskSignalDettlaffColumnsDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskSignalDettlaffColumnsDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}