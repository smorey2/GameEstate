using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CCustomCameraRopePPC : ICustomCameraPivotPositionController
	{
		[Ordinal(1)] [RED("dampFactor")] 		public CFloat DampFactor { get; set;}

		[Ordinal(2)] [RED("smoothZ")] 		public CFloat SmoothZ { get; set;}

		[Ordinal(3)] [RED("ropeLength")] 		public CFloat RopeLength { get; set;}

		public CCustomCameraRopePPC(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CCustomCameraRopePPC(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}