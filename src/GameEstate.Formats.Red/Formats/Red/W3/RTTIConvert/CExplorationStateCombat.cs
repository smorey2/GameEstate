using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CExplorationStateCombat : CExplorationStateAbstract
	{
		[Ordinal(1)] [RED("m_TimeToSlideNeededF")] 		public CFloat M_TimeToSlideNeededF { get; set;}

		[Ordinal(2)] [RED("m_TimeToSlideCurF")] 		public CFloat M_TimeToSlideCurF { get; set;}

		[Ordinal(3)] [RED("m_FallHasToWaitForCombatAction")] 		public CBool M_FallHasToWaitForCombatAction { get; set;}

		[Ordinal(4)] [RED("m_SlideHasToWaitForCombatAction")] 		public CBool M_SlideHasToWaitForCombatAction { get; set;}

		[Ordinal(5)] [RED("m_FallHorizontalImpulseCancelF")] 		public CFloat M_FallHorizontalImpulseCancelF { get; set;}

		[Ordinal(6)] [RED("m_FallHorizontalImpulseF")] 		public CFloat M_FallHorizontalImpulseF { get; set;}

		[Ordinal(7)] [RED("m_FallExtraVerticalImpulseF")] 		public CFloat M_FallExtraVerticalImpulseF { get; set;}

		[Ordinal(8)] [RED("m_TurnAdjustTimeSprintF")] 		public CFloat M_TurnAdjustTimeSprintF { get; set;}

		public CExplorationStateCombat(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CExplorationStateCombat(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}