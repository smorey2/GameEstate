using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CGrassCellMask : CVariable
	{
		[Ordinal(1)] [RED("srtFileName")] 		public CString SrtFileName { get; set;}

		[Ordinal(2)] [RED("firstRow")] 		public CInt32 FirstRow { get; set;}

		[Ordinal(3)] [RED("lastRow")] 		public CInt32 LastRow { get; set;}

		[Ordinal(4)] [RED("firstCol")] 		public CInt32 FirstCol { get; set;}

		[Ordinal(5)] [RED("lastCol")] 		public CInt32 LastCol { get; set;}

		[Ordinal(6)] [RED("cellSize")] 		public CFloat CellSize { get; set;}

		[Ordinal(7)] [RED("bitmap")] 		public LongBitField Bitmap { get; set;}

		public CGrassCellMask(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CGrassCellMask(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}