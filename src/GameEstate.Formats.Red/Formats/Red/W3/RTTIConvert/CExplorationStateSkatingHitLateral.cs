using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CExplorationStateSkatingHitLateral : CExplorationStateAbstract
	{
		[Ordinal(1)] [RED("skateGlobal")] 		public CHandle<CExplorationSkatingGlobal> SkateGlobal { get; set;}

		[Ordinal(2)] [RED("behAnimEnd")] 		public CName BehAnimEnd { get; set;}

		[Ordinal(3)] [RED("timeMax")] 		public CFloat TimeMax { get; set;}

		[Ordinal(4)] [RED("speedMinToEnter")] 		public CFloat SpeedMinToEnter { get; set;}

		[Ordinal(5)] [RED("speedReductionPerc")] 		public CFloat SpeedReductionPerc { get; set;}

		[Ordinal(6)] [RED("extraAngle")] 		public CFloat ExtraAngle { get; set;}

		public CExplorationStateSkatingHitLateral(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CExplorationStateSkatingHitLateral(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}