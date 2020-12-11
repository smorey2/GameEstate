using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class IBehTreeNodeAtomicFlyToBaseDefinition : IBehTreeNodeAtomicFlightDefinition
	{
		[Ordinal(1)] [RED("skipHeightCheck")] 		public CBool SkipHeightCheck { get; set;}

		[Ordinal(2)] [RED("useAbsoluteHeightDifference")] 		public CBool UseAbsoluteHeightDifference { get; set;}

		[Ordinal(3)] [RED("checkDistanceWithoutOffsets")] 		public CBool CheckDistanceWithoutOffsets { get; set;}

		[Ordinal(4)] [RED("distanceOffset")] 		public CBehTreeValFloat DistanceOffset { get; set;}

		[Ordinal(5)] [RED("heightOffset")] 		public CBehTreeValFloat HeightOffset { get; set;}

		[Ordinal(6)] [RED("min2DDistance")] 		public CBehTreeValFloat Min2DDistance { get; set;}

		[Ordinal(7)] [RED("minHeight")] 		public CBehTreeValFloat MinHeight { get; set;}

		public IBehTreeNodeAtomicFlyToBaseDefinition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new IBehTreeNodeAtomicFlyToBaseDefinition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}