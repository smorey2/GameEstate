using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CExplorationClimbOracle : CObject
	{
		[Ordinal(1)] [RED("m_ExplorationO")] 		public CHandle<CExplorationStateManager> M_ExplorationO { get; set;}

		[Ordinal(2)] [RED("probeTop")] 		public CHandle<CClimbProbe> ProbeTop { get; set;}

		[Ordinal(3)] [RED("probeBottom")] 		public CHandle<CClimbProbe> ProbeBottom { get; set;}

		[Ordinal(4)] [RED("distForwardToCheck")] 		public CFloat DistForwardToCheck { get; set;}

		[Ordinal(5)] [RED("characterRadius")] 		public CFloat CharacterRadius { get; set;}

		[Ordinal(6)] [RED("characterHeight")] 		public CFloat CharacterHeight { get; set;}

		[Ordinal(7)] [RED("radiusToCheck")] 		public CFloat RadiusToCheck { get; set;}

		[Ordinal(8)] [RED("bottomCheckAllowed")] 		public CBool BottomCheckAllowed { get; set;}

		[Ordinal(9)] [RED("topIsPriority")] 		public CBool TopIsPriority { get; set;}

		[Ordinal(10)] [RED("probeBeingUsed")] 		public CEnum<EClimbProbeUsed> ProbeBeingUsed { get; set;}

		[Ordinal(11)] [RED("debugLogFails")] 		public CBool DebugLogFails { get; set;}

		[Ordinal(12)] [RED("vectorUp")] 		public Vector VectorUp { get; set;}

		public CExplorationClimbOracle(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CExplorationClimbOracle(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}