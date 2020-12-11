using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CExplorationStateSkateStopFast : CExplorationStateSkatingDrift
	{
		[Ordinal(1)] [RED("inputRangeToEnter")] 		public CFloat InputRangeToEnter { get; set;}

		[Ordinal(2)] [RED("duration")] 		public CFloat Duration { get; set;}

		[Ordinal(3)] [RED("speedStopCurve")] 		public CHandle<CCurve> SpeedStopCurve { get; set;}

		public CExplorationStateSkateStopFast(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CExplorationStateSkateStopFast(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}