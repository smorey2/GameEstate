using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SMapPinType : CVariable
	{
		[Ordinal(1)] [RED("typeName")] 		public CName TypeName { get; set;}

		[Ordinal(2)] [RED("icon")] 		public CString Icon { get; set;}

		[Ordinal(3)] [RED("radius")] 		public CFloat Radius { get; set;}

		[Ordinal(4)] [RED("color")] 		public CColor Color { get; set;}

		public SMapPinType(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SMapPinType(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}