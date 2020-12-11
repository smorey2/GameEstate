using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3TutorialManagerUIHandlerStateMutationsUnlockedSkillSlot : W3TutorialManagerUIHandlerStateTutHandlerBaseState
	{
		[Ordinal(1)] [RED("LEVEL_UP")] 		public CName LEVEL_UP { get; set;}

		[Ordinal(2)] [RED("OPEN_CHAR_PANEL")] 		public CName OPEN_CHAR_PANEL { get; set;}

		[Ordinal(3)] [RED("isClosing")] 		public CBool IsClosing { get; set;}

		[Ordinal(4)] [RED("selectedMutation")] 		public CEnum<EPlayerMutationType> SelectedMutation { get; set;}

		public W3TutorialManagerUIHandlerStateMutationsUnlockedSkillSlot(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3TutorialManagerUIHandlerStateMutationsUnlockedSkillSlot(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}