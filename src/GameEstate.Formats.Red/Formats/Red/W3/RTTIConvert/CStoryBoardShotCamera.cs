using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CStoryBoardShotCamera : CStaticCamera
	{
		[Ordinal(1)] [RED("settings")] 		public SStoryBoardCameraSettings Settings { get; set;}

		[Ordinal(2)] [RED("comp")] 		public CHandle<CCameraComponent> Comp { get; set;}

		[Ordinal(3)] [RED("env")] 		public CHandle<CEnvironmentDefinition> Env { get; set;}

		[Ordinal(4)] [RED("gameDofSettings")] 		public SStoryBoardCameraDofSettings GameDofSettings { get; set;}

		public CStoryBoardShotCamera(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CStoryBoardShotCamera(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}