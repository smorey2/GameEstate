using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CPoseCompressionDefault : IPoseCompression
	{
		[Ordinal(1)] [RED("firstRotBoneName")] 		public CString FirstRotBoneName { get; set;}

		[Ordinal(2)] [RED("lastRotBoneName")] 		public CString LastRotBoneName { get; set;}

		[Ordinal(3)] [RED("firstTransBoneName")] 		public CString FirstTransBoneName { get; set;}

		[Ordinal(4)] [RED("lastTransBoneName")] 		public CString LastTransBoneName { get; set;}

		[Ordinal(5)] [RED("firstRotBone")] 		public CInt32 FirstRotBone { get; set;}

		[Ordinal(6)] [RED("lastRotBone")] 		public CInt32 LastRotBone { get; set;}

		[Ordinal(7)] [RED("firstTransBone")] 		public CInt32 FirstTransBone { get; set;}

		[Ordinal(8)] [RED("lastTransBone")] 		public CInt32 LastTransBone { get; set;}

		[Ordinal(9)] [RED("compressTranslationType")] 		public CEnum<ECompressTranslationType> CompressTranslationType { get; set;}

		public CPoseCompressionDefault(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CPoseCompressionDefault(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}