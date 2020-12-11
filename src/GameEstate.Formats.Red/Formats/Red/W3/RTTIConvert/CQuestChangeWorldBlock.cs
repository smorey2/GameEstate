using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CQuestChangeWorldBlock : CQuestGraphBlock
	{
		[Ordinal(1)] [RED("worldFilePath")] 		public CString WorldFilePath { get; set;}

		[Ordinal(2)] [RED("newWorld")] 		public CInt32 NewWorld { get; set;}

		[Ordinal(3)] [RED("loadingMovieName")] 		public CString LoadingMovieName { get; set;}

		[Ordinal(4)] [RED("targetTag")] 		public TagList TargetTag { get; set;}

		public CQuestChangeWorldBlock(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CQuestChangeWorldBlock(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}