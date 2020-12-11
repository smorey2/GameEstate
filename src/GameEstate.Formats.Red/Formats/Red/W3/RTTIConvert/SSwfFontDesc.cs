using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SSwfFontDesc : CVariable
	{
		[Ordinal(1)] [RED("fontName")] 		public CString FontName { get; set;}

		[Ordinal(2)] [RED("numGlyphs")] 		public CUInt32 NumGlyphs { get; set;}

		[Ordinal(3)] [RED("italic")] 		public CBool Italic { get; set;}

		[Ordinal(4)] [RED("bold")] 		public CBool Bold { get; set;}

		public SSwfFontDesc(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SSwfFontDesc(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}