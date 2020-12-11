using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SAnimationBufferBitwiseCompressedData : CVariable
	{
		[Ordinal(1)] [RED("dt")] 		public CFloat Dt { get; set;}

		[Ordinal(2)] [RED("compression")] 		public CInt8 Compression { get; set;}

		[Ordinal(3)] [RED("numFrames")] 		public CUInt16 NumFrames { get; set;}

		[Ordinal(4)] [RED("dataAddr")] 		public CUInt32 DataAddr { get; set;}

		[Ordinal(5)] [RED("dataAddrFallback")] 		public CUInt32 DataAddrFallback { get; set;}

		public SAnimationBufferBitwiseCompressedData(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SAnimationBufferBitwiseCompressedData(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}