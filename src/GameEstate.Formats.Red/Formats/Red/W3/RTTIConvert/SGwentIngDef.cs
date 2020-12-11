using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SGwentIngDef : CVariable
	{
		[Ordinal(1)] [RED("itemName")] 		public CName ItemName { get; set;}

		[Ordinal(2)] [RED("reqLevel")] 		public CInt32 ReqLevel { get; set;}

		[Ordinal(3)] [RED("quantityMin")] 		public CInt32 QuantityMin { get; set;}

		[Ordinal(4)] [RED("quantityMax")] 		public CInt32 QuantityMax { get; set;}

		public SGwentIngDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SGwentIngDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}