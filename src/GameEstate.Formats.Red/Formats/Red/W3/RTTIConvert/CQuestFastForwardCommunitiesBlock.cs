using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CQuestFastForwardCommunitiesBlock : CQuestGraphBlock
	{
		[Ordinal(1)] [RED("manageBlackscreen")] 		public CBool ManageBlackscreen { get; set;}

		[Ordinal(2)] [RED("respawnEveryone")] 		public CBool RespawnEveryone { get; set;}

		[Ordinal(3)] [RED("dontSpawnHostilesClose")] 		public CBool DontSpawnHostilesClose { get; set;}

		[Ordinal(4)] [RED("timeLimit")] 		public CFloat TimeLimit { get; set;}

		public CQuestFastForwardCommunitiesBlock(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CQuestFastForwardCommunitiesBlock(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}