using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CWorldMap : CResource
	{
		[Ordinal(1)] [RED("guid")] 		public CGUID Guid { get; set;}

		[Ordinal(2)] [RED("displayTitle")] 		public LocalizedString DisplayTitle { get; set;}

		[Ordinal(3)] [RED("imageInfo")] 		public SWorldMapImageInfo ImageInfo { get; set;}

		[Ordinal(4)] [RED("staticMapPins", 2,0)] 		public CArray<SStaticMapPin> StaticMapPins { get; set;}

		public CWorldMap(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CWorldMap(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}