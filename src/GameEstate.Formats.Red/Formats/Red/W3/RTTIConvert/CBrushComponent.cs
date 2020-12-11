using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBrushComponent : CDrawableComponent
	{
		[Ordinal(1)] [RED("brushIndex")] 		public CInt32 BrushIndex { get; set;}

		[Ordinal(2)] [RED("csgType")] 		public CEnum<EBrushCSGType> CsgType { get; set;}

		[Ordinal(3)] [RED("faces", 2,0)] 		public CArray<CPtr<CBrushFace>> Faces { get; set;}

		public CBrushComponent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBrushComponent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}