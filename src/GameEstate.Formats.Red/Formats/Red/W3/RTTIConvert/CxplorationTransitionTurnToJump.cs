using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CxplorationTransitionTurnToJump : CExplorationStateTransitionAbstract
	{
		[Ordinal(1)] [RED("timeToExit")] 		public CFloat TimeToExit { get; set;}

		[Ordinal(2)] [RED("angleToTrigger")] 		public CFloat AngleToTrigger { get; set;}

		public CxplorationTransitionTurnToJump(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CxplorationTransitionTurnToJump(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}