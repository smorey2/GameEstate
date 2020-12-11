using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CModStoryBoardOverviewModeStateSbUi_OverviewShotManaging : CScriptableState
	{
		[Ordinal(1)] [RED("storyboard")] 		public CHandle<CModStoryBoard> Storyboard { get; set;}

		[Ordinal(2)] [RED("moveInsteadSwitch")] 		public CBool MoveInsteadSwitch { get; set;}

		public CModStoryBoardOverviewModeStateSbUi_OverviewShotManaging(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CModStoryBoardOverviewModeStateSbUi_OverviewShotManaging(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}