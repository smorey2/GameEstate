using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SCustomCameraParams : CVariable
	{
		[Ordinal(1)] [RED("source")] 		public CHandle<CActor> Source { get; set;}

		[Ordinal(2)] [RED("useCustomCamera")] 		public CBool UseCustomCamera { get; set;}

		[Ordinal(3)] [RED("cameraParams")] 		public SMultiValue CameraParams { get; set;}

		public SCustomCameraParams(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SCustomCameraParams(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}