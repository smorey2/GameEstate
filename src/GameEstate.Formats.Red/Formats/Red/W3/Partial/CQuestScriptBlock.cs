using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public partial class CQuestScriptBlock : CQuestGraphBlock
	{
		[Ordinal(1)] [RED("functionName")] 		public CName FunctionName { get; set;}

		[Ordinal(2)] [RED("saveMode")] 		public CEnum<EQuestScriptSaveMode> SaveMode { get; set;}

		[Ordinal(3)] [RED("parameters", 2,0)] 		public CArray<QuestScriptParam> Parameters { get; set;}

		[Ordinal(4)] [RED("choiceOutput")] 		public CBool ChoiceOutput { get; set;}

		[Ordinal(5)] [RED("caption")] 		public CString Caption { get; set;}

		//public CQuestScriptBlock(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CQuestScriptBlock(cr2w, parent, name);

		//public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		//public override void Write(BinaryWriter file) => base.Write(file);

	}
}