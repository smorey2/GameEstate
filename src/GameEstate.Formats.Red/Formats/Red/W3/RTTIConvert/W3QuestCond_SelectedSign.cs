using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3QuestCond_SelectedSign : CQuestScriptedCondition
	{
		[Ordinal(1)] [RED("signType")] 		public CEnum<ESignType> SignType { get; set;}

		[Ordinal(2)] [RED("isFulfilled")] 		public CBool IsFulfilled { get; set;}

		[Ordinal(3)] [RED("listener")] 		public CHandle<W3QuestCond_SelectedSign_Listener> Listener { get; set;}

		[Ordinal(4)] [RED("factsNames", 2,0)] 		public CArray<CString> FactsNames { get; set;}

		public W3QuestCond_SelectedSign(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3QuestCond_SelectedSign(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}