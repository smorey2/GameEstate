using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SSwfHeaderInfo : CVariable
	{
		[Ordinal(1)] [RED("frameRate")] 		public CFloat FrameRate { get; set;}

		[Ordinal(2)] [RED("frameHeight")] 		public CFloat FrameHeight { get; set;}

		[Ordinal(3)] [RED("frameWidth")] 		public CFloat FrameWidth { get; set;}

		[Ordinal(4)] [RED("frameCount")] 		public CUInt32 FrameCount { get; set;}

		[Ordinal(5)] [RED("height")] 		public CFloat Height { get; set;}

		[Ordinal(6)] [RED("width")] 		public CFloat Width { get; set;}

		[Ordinal(7)] [RED("version")] 		public CUInt32 Version { get; set;}

		[Ordinal(8)] [RED("compressed")] 		public CBool Compressed { get; set;}

		public SSwfHeaderInfo(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SSwfHeaderInfo(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}