using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3QuestCond_GlobalAttitude : CQuestScriptedCondition
	{
		[Ordinal(1)] [RED("srcGroup")] 		public CName SrcGroup { get; set;}

		[Ordinal(2)] [RED("dstGroup")] 		public CName DstGroup { get; set;}

		[Ordinal(3)] [RED("attitude")] 		public CEnum<EAIAttitude> Attitude { get; set;}

		public W3QuestCond_GlobalAttitude(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3QuestCond_GlobalAttitude(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}