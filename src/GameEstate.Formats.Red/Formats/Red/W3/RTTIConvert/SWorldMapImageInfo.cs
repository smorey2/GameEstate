using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SWorldMapImageInfo : CVariable
	{
		[Ordinal(1)] [RED("cropRect")] 		public Rect CropRect { get; set;}

		[Ordinal(2)] [RED("baseFileName")] 		public CString BaseFileName { get; set;}

		[Ordinal(3)] [RED("height")] 		public CInt32 Height { get; set;}

		[Ordinal(4)] [RED("width")] 		public CInt32 Width { get; set;}

		public SWorldMapImageInfo(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SWorldMapImageInfo(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}