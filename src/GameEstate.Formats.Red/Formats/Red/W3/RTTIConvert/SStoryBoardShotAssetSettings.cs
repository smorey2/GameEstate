using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SStoryBoardShotAssetSettings : CVariable
	{
		[Ordinal(1)] [RED("assetId")] 		public CString AssetId { get; set;}

		[Ordinal(2)] [RED("placement")] 		public SStoryBoardPlacementSettings Placement { get; set;}

		[Ordinal(3)] [RED("pose")] 		public SStoryBoardPoseSettings Pose { get; set;}

		[Ordinal(4)] [RED("animation")] 		public SStoryBoardAnimationSettings Animation { get; set;}

		[Ordinal(5)] [RED("mimics")] 		public SStoryBoardAnimationSettings Mimics { get; set;}

		[Ordinal(6)] [RED("lookAt")] 		public SStoryBoardLookAtSettings LookAt { get; set;}

		[Ordinal(7)] [RED("audio")] 		public SStoryBoardAudioSettings Audio { get; set;}

		public SStoryBoardShotAssetSettings(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SStoryBoardShotAssetSettings(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}