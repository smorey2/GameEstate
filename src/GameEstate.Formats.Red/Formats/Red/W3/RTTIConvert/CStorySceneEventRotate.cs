using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CStorySceneEventRotate : CStorySceneEvent
	{
		[Ordinal(1)] [RED("actor")] 		public CName Actor { get; set;}

		[Ordinal(2)] [RED("angle")] 		public CFloat Angle { get; set;}

		[Ordinal(3)] [RED("absoluteAngle")] 		public CBool AbsoluteAngle { get; set;}

		[Ordinal(4)] [RED("toCamera")] 		public CBool ToCamera { get; set;}

		[Ordinal(5)] [RED("instant")] 		public CBool Instant { get; set;}

		public CStorySceneEventRotate(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CStorySceneEventRotate(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}