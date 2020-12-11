using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CDecalComponent : CDrawableComponent
	{
		[Ordinal(1)] [RED("diffuseTexture")] 		public CHandle<CBitmapTexture> DiffuseTexture { get; set;}

		[Ordinal(2)] [RED("specularity")] 		public CFloat Specularity { get; set;}

		[Ordinal(3)] [RED("specularColor")] 		public CColor SpecularColor { get; set;}

		[Ordinal(4)] [RED("normalThreshold")] 		public CFloat NormalThreshold { get; set;}

		[Ordinal(5)] [RED("autoHideDistance")] 		public CFloat AutoHideDistance { get; set;}

		[Ordinal(6)] [RED("verticalFlip")] 		public CBool VerticalFlip { get; set;}

		[Ordinal(7)] [RED("horizontalFlip")] 		public CBool HorizontalFlip { get; set;}

		[Ordinal(8)] [RED("fadeTime")] 		public CFloat FadeTime { get; set;}

		public CDecalComponent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CDecalComponent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}