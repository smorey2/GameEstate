using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAnimationBufferMultipart : IAnimationBuffer
	{
		[Ordinal(1)] [RED("numFrames")] 		public CUInt32 NumFrames { get; set;}

		[Ordinal(2)] [RED("numBones")] 		public CUInt32 NumBones { get; set;}

		[Ordinal(3)] [RED("numTracks")] 		public CUInt32 NumTracks { get; set;}

		[Ordinal(4)] [RED("firstFrames", 2,0)] 		public CArray<CUInt32> FirstFrames { get; set;}

		[Ordinal(5)] [RED("parts", 2,0)] 		public CArray<CPtr<IAnimationBuffer>> Parts { get; set;}

		public CAnimationBufferMultipart(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAnimationBufferMultipart(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}