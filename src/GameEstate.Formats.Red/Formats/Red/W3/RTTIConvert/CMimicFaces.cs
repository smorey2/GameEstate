using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CMimicFaces : CResource
	{
		[Ordinal(1)] [RED("mimicSkeleton")] 		public CHandle<CSkeleton> MimicSkeleton { get; set;}

		[Ordinal(2)] [RED("mimicPoses", 2,0)] 		public CArray<CArray<EngineQsTransform>> MimicPoses { get; set;}

		[Ordinal(3)] [RED("mapping", 2,0)] 		public CArray<CInt32> Mapping { get; set;}

		[Ordinal(4)] [RED("neckIndex")] 		public CInt32 NeckIndex { get; set;}

		[Ordinal(5)] [RED("headIndex")] 		public CInt32 HeadIndex { get; set;}

		[Ordinal(6)] [RED("normalBlendAreas", 2,0)] 		public CArray<Vector> NormalBlendAreas { get; set;}

		public CMimicFaces(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CMimicFaces(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}