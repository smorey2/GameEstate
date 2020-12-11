using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3TutorialManagerUIHandlerStateMutationsCharPanelUnlockedSkillSlot : W3TutorialManagerUIHandlerStateTutHandlerBaseState
	{
		[Ordinal(1)] [RED("ADDITIONAL_SLOT")] 		public CName ADDITIONAL_SLOT { get; set;}

		[Ordinal(2)] [RED("COLOR")] 		public CName COLOR { get; set;}

		[Ordinal(3)] [RED("YELLOW")] 		public CName YELLOW { get; set;}

		[Ordinal(4)] [RED("isClosing")] 		public CBool IsClosing { get; set;}

		[Ordinal(5)] [RED("selectedMutation")] 		public CEnum<EPlayerMutationType> SelectedMutation { get; set;}

		public W3TutorialManagerUIHandlerStateMutationsCharPanelUnlockedSkillSlot(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3TutorialManagerUIHandlerStateMutationsCharPanelUnlockedSkillSlot(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}