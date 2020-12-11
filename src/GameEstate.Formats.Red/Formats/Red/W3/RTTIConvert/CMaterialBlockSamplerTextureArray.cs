using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CMaterialBlockSamplerTextureArray : CMaterialBlockSampler
	{
		[Ordinal(1)] [RED("subUVWidth")] 		public CUInt32 SubUVWidth { get; set;}

		[Ordinal(2)] [RED("subUVHeight")] 		public CUInt32 SubUVHeight { get; set;}

		[Ordinal(3)] [RED("subUVInterpolate")] 		public CBool SubUVInterpolate { get; set;}

		[Ordinal(4)] [RED("projected")] 		public CBool Projected { get; set;}

		public CMaterialBlockSamplerTextureArray(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CMaterialBlockSamplerTextureArray(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}