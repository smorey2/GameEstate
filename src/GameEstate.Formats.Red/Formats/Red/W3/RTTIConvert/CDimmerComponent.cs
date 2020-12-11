using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CDimmerComponent : CDrawableComponent
	{
		[Ordinal(1)] [RED("isAreaMarker")] 		public CBool IsAreaMarker { get; set;}

		[Ordinal(2)] [RED("dimmerType")] 		public CEnum<EDimmerType> DimmerType { get; set;}

		[Ordinal(3)] [RED("ambientLevel")] 		public CFloat AmbientLevel { get; set;}

		[Ordinal(4)] [RED("marginFactor")] 		public CFloat MarginFactor { get; set;}

		[Ordinal(5)] [RED("autoHideDistance")] 		public CFloat AutoHideDistance { get; set;}

		public CDimmerComponent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CDimmerComponent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}