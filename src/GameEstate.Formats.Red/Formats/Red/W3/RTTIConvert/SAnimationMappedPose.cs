using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SAnimationMappedPose : CVariable
	{
		[Ordinal(1)] [RED("bones", 133,0)] 		public CArray<EngineQsTransform> Bones { get; set;}

		[Ordinal(2)] [RED("tracks", 2,0)] 		public CArray<CFloat> Tracks { get; set;}

		[Ordinal(3)] [RED("bonesMapping", 2,0)] 		public CArray<CInt32> BonesMapping { get; set;}

		[Ordinal(4)] [RED("tracksMapping", 2,0)] 		public CArray<CInt32> TracksMapping { get; set;}

		[Ordinal(5)] [RED("weight")] 		public CFloat Weight { get; set;}

		[Ordinal(6)] [RED("mode")] 		public CEnum<ESAnimationMappedPoseMode> Mode { get; set;}

		[Ordinal(7)] [RED("correctionID")] 		public CGUID CorrectionID { get; set;}

		[Ordinal(8)] [RED("correctionIdleID")] 		public CName CorrectionIdleID { get; set;}

		public SAnimationMappedPose(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SAnimationMappedPose(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}