using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SFlashRenderTargetCamera : CVariable
	{
		[Ordinal(1)] [RED("position")] 		public Vector Position { get; set;}

		[Ordinal(2)] [RED("rotation")] 		public EulerAngles Rotation { get; set;}

		[Ordinal(3)] [RED("fov")] 		public CFloat Fov { get; set;}

		[Ordinal(4)] [RED("zoom")] 		public CFloat Zoom { get; set;}

		public SFlashRenderTargetCamera(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SFlashRenderTargetCamera(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}