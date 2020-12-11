using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SStoryBoardAnimationInfo : CVariable
	{
		[Ordinal(1)] [RED("path")] 		public CString Path { get; set;}

		[Ordinal(2)] [RED("cat1")] 		public CString Cat1 { get; set;}

		[Ordinal(3)] [RED("cat2")] 		public CString Cat2 { get; set;}

		[Ordinal(4)] [RED("cat3")] 		public CString Cat3 { get; set;}

		[Ordinal(5)] [RED("id")] 		public CName Id { get; set;}

		[Ordinal(6)] [RED("caption")] 		public CString Caption { get; set;}

		[Ordinal(7)] [RED("frames")] 		public CInt32 Frames { get; set;}

		public SStoryBoardAnimationInfo(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SStoryBoardAnimationInfo(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}