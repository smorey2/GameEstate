using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CStorySceneEventCameraLight : CStorySceneEvent
	{
		[Ordinal(1)] [RED("cameralightType")] 		public CEnum<ECameraLightModType> CameralightType { get; set;}

		[Ordinal(2)] [RED("lightMod1")] 		public SStorySceneCameraLightMod LightMod1 { get; set;}

		[Ordinal(3)] [RED("lightMod2")] 		public SStorySceneCameraLightMod LightMod2 { get; set;}

		public CStorySceneEventCameraLight(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CStorySceneEventCameraLight(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}