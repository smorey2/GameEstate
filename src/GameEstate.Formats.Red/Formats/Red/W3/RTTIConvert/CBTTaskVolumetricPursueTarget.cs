using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskVolumetricPursueTarget : CBTTaskVolumetricMove
	{
		[Ordinal(1)] [RED("distanceOffset")] 		public CFloat DistanceOffset { get; set;}

		[Ordinal(2)] [RED("heightOffset")] 		public CFloat HeightOffset { get; set;}

		[Ordinal(3)] [RED("minDistance")] 		public CFloat MinDistance { get; set;}

		[Ordinal(4)] [RED("minHeight")] 		public CFloat MinHeight { get; set;}

		[Ordinal(5)] [RED("completeWithSucces")] 		public CBool CompleteWithSucces { get; set;}

		[Ordinal(6)] [RED("useAbsoluteHeightDifference")] 		public CBool UseAbsoluteHeightDifference { get; set;}

		[Ordinal(7)] [RED("checkDistanceWithoutOffsets")] 		public CBool CheckDistanceWithoutOffsets { get; set;}

		[Ordinal(8)] [RED("skipHeightCheck")] 		public CBool SkipHeightCheck { get; set;}

		[Ordinal(9)] [RED("distanceDiff")] 		public CFloat DistanceDiff { get; set;}

		[Ordinal(10)] [RED("heightDiff")] 		public CFloat HeightDiff { get; set;}

		[Ordinal(11)] [RED("isMinHeightNegative")] 		public CBool IsMinHeightNegative { get; set;}

		public CBTTaskVolumetricPursueTarget(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskVolumetricPursueTarget(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}