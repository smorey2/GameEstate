using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3SummonedEntityComponent : CScriptedComponent
	{
		[Ordinal(1)] [RED("m_Summoner")] 		public CHandle<CActor> M_Summoner { get; set;}

		[Ordinal(2)] [RED("m_SummonedTime")] 		public CFloat M_SummonedTime { get; set;}

		[Ordinal(3)] [RED("shouldUseSummonerGuardArea")] 		public CBool ShouldUseSummonerGuardArea { get; set;}

		[Ordinal(4)] [RED("killOnSummonersDeath")] 		public CBool KillOnSummonersDeath { get; set;}

		public W3SummonedEntityComponent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3SummonedEntityComponent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}