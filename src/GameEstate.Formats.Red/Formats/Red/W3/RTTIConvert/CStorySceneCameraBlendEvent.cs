using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CStorySceneCameraBlendEvent : CStorySceneEventBlend
	{
		[Ordinal(1)] [RED("firstPointOfInterpolation")] 		public CFloat FirstPointOfInterpolation { get; set;}

		[Ordinal(2)] [RED("lastPointOfInterpolation")] 		public CFloat LastPointOfInterpolation { get; set;}

		[Ordinal(3)] [RED("firstPartInterpolation")] 		public CEnum<ECameraInterpolation> FirstPartInterpolation { get; set;}

		[Ordinal(4)] [RED("lastPartInterpolation")] 		public CEnum<ECameraInterpolation> LastPartInterpolation { get; set;}

		public CStorySceneCameraBlendEvent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CStorySceneCameraBlendEvent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}