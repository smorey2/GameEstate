using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CCommentGraphBlock : CGraphHelperBlock
	{
		[Ordinal(1)] [RED("size")] 		public Vector Size { get; set;}

		[Ordinal(2)] [RED("commentGraphBlockText")] 		public CString CommentGraphBlockText { get; set;}

		[Ordinal(3)] [RED("titleColor")] 		public CColor TitleColor { get; set;}

		public CCommentGraphBlock(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CCommentGraphBlock(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}