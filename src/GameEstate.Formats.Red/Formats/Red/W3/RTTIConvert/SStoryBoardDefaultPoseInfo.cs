using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SStoryBoardDefaultPoseInfo : CVariable
	{
		[Ordinal(1)] [RED("uId")] 		public CInt32 UId { get; set;}

		[Ordinal(2)] [RED("type")] 		public CInt32 Type { get; set;}

		[Ordinal(3)] [RED("animId")] 		public CName AnimId { get; set;}

		public SStoryBoardDefaultPoseInfo(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SStoryBoardDefaultPoseInfo(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}