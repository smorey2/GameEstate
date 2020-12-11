using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CMaterialBlockSamplerNormalDetail : CMaterialBlockSampler
	{
		[Ordinal(1)] [RED("scale")] 		public Vector Scale { get; set;}

		[Ordinal(2)] [RED("sampleTangentSpace")] 		public CBool SampleTangentSpace { get; set;}

		[Ordinal(3)] [RED("BlendStartDistance")] 		public CFloat BlendStartDistance { get; set;}

		[Ordinal(4)] [RED("BlendEndDistance")] 		public CFloat BlendEndDistance { get; set;}

		public CMaterialBlockSamplerNormalDetail(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CMaterialBlockSamplerNormalDetail(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}