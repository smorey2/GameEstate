using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class STargetSelectionWeights : CVariable
	{
		[Ordinal(1)] [RED("angleWeight")] 		public CFloat AngleWeight { get; set;}

		[Ordinal(2)] [RED("distanceWeight")] 		public CFloat DistanceWeight { get; set;}

		[Ordinal(3)] [RED("distanceRingWeight")] 		public CFloat DistanceRingWeight { get; set;}

		public STargetSelectionWeights(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new STargetSelectionWeights(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}