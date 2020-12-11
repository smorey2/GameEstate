using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CExplorationStateSkatingDrift : CExplorationStateAbstract
	{
		[Ordinal(1)] [RED("skateGlobal")] 		public CHandle<CExplorationSkatingGlobal> SkateGlobal { get; set;}

		[Ordinal(2)] [RED("impulse")] 		public CFloat Impulse { get; set;}

		[Ordinal(3)] [RED("impulseSpeedMax")] 		public CFloat ImpulseSpeedMax { get; set;}

		[Ordinal(4)] [RED("sharpTurn")] 		public CBool SharpTurn { get; set;}

		[Ordinal(5)] [RED("sharpTurnTime")] 		public CFloat SharpTurnTime { get; set;}

		[Ordinal(6)] [RED("sharpTurnSpeed")] 		public CFloat SharpTurnSpeed { get; set;}

		[Ordinal(7)] [RED("holdTurnSpeed")] 		public CFloat HoldTurnSpeed { get; set;}

		[Ordinal(8)] [RED("chainTimeToDrift")] 		public CFloat ChainTimeToDrift { get; set;}

		[Ordinal(9)] [RED("exiting")] 		public CBool Exiting { get; set;}

		[Ordinal(10)] [RED("timeEndingMax")] 		public CFloat TimeEndingMax { get; set;}

		[Ordinal(11)] [RED("timeEndingFlow")] 		public CBool TimeEndingFlow { get; set;}

		[Ordinal(12)] [RED("timeEndingCur")] 		public CFloat TimeEndingCur { get; set;}

		[Ordinal(13)] [RED("behDriftRestart")] 		public CName BehDriftRestart { get; set;}

		[Ordinal(14)] [RED("behDriftEnd")] 		public CName BehDriftEnd { get; set;}

		[Ordinal(15)] [RED("behDriftLeftSide")] 		public CName BehDriftLeftSide { get; set;}

		[Ordinal(16)] [RED("sideIsLeft")] 		public CBool SideIsLeft { get; set;}

		public CExplorationStateSkatingDrift(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CExplorationStateSkatingDrift(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}