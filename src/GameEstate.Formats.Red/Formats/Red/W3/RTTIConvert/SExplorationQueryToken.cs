using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SExplorationQueryToken : CVariable
	{
		[Ordinal(1)] [RED("valid")] 		public CBool Valid { get; set;}

		[Ordinal(2)] [RED("type")] 		public CEnum<EExplorationType> Type { get; set;}

		[Ordinal(3)] [RED("pointOnEdge")] 		public Vector PointOnEdge { get; set;}

		[Ordinal(4)] [RED("normal")] 		public Vector Normal { get; set;}

		[Ordinal(5)] [RED("usesHands")] 		public CBool UsesHands { get; set;}

		public SExplorationQueryToken(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SExplorationQueryToken(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}