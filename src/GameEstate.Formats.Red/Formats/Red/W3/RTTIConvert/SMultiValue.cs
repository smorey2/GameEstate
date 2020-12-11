using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SMultiValue : CVariable
	{
		[Ordinal(1)] [RED("floats", 2,0)] 		public CArray<CFloat> Floats { get; set;}

		[Ordinal(2)] [RED("bools", 2,0)] 		public CArray<CBool> Bools { get; set;}

		[Ordinal(3)] [RED("enums", 2,0)] 		public CArray<SEnumVariant> Enums { get; set;}

		[Ordinal(4)] [RED("names", 2,0)] 		public CArray<CName> Names { get; set;}

		public SMultiValue(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SMultiValue(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}