using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class ICustomCameraPositionController : ICustomCameraBaseController
	{
		[Ordinal(1)] [RED("enableAutoCollisionAvoidance")] 		public CBool EnableAutoCollisionAvoidance { get; set;}

		[Ordinal(2)] [RED("enableScreenSpaceCorrections")] 		public CBool EnableScreenSpaceCorrections { get; set;}

		public ICustomCameraPositionController(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new ICustomCameraPositionController(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}