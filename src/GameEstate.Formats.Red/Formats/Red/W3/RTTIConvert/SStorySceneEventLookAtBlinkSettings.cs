using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SStorySceneEventLookAtBlinkSettings : CVariable
	{
		[Ordinal(1)] [RED("canCloseEyes")] 		public CBool CanCloseEyes { get; set;}

		[Ordinal(2)] [RED("forceCloseEyes")] 		public CBool ForceCloseEyes { get; set;}

		[Ordinal(3)] [RED("animationName")] 		public CName AnimationName { get; set;}

		[Ordinal(4)] [RED("startOffset")] 		public CFloat StartOffset { get; set;}

		[Ordinal(5)] [RED("durationPercent")] 		public CFloat DurationPercent { get; set;}

		[Ordinal(6)] [RED("horizontalAngleDeg")] 		public CFloat HorizontalAngleDeg { get; set;}

		public SStorySceneEventLookAtBlinkSettings(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SStorySceneEventLookAtBlinkSettings(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}