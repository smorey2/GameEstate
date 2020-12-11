using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAIIdleSpontanousFormationTree : IAIIdleFormationTree
	{
		[Ordinal(1)] [RED("partyMemberName")] 		public CName PartyMemberName { get; set;}

		[Ordinal(2)] [RED("leaderSteering")] 		public CHandle<CMoveSteeringBehavior> LeaderSteering { get; set;}

		[Ordinal(3)] [RED("leadFormationTree")] 		public CHandle<CAIIdleTree> LeadFormationTree { get; set;}

		[Ordinal(4)] [RED("loneWolfTree")] 		public CHandle<CAIIdleTree> LoneWolfTree { get; set;}

		public CAIIdleSpontanousFormationTree(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAIIdleSpontanousFormationTree(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}