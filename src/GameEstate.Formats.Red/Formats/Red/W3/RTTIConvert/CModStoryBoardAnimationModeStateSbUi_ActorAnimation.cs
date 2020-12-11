using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CModStoryBoardAnimationModeStateSbUi_ActorAnimation : CModSbListViewWorkModeStateSbUi_FilteredListSelect
	{
		[Ordinal(1)] [RED("actor")] 		public CHandle<CModStoryBoardActor> Actor { get; set;}

		[Ordinal(2)] [RED("newAnimation")] 		public SStoryBoardAnimationSettings NewAnimation { get; set;}

		[Ordinal(3)] [RED("animStateCallback")] 		public CHandle<CModSbUiAnimModeAnimStateCallback> AnimStateCallback { get; set;}

		public CModStoryBoardAnimationModeStateSbUi_ActorAnimation(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CModStoryBoardAnimationModeStateSbUi_ActorAnimation(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}