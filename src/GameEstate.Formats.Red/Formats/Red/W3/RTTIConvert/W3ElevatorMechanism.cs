using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3ElevatorMechanism : CEntity
	{
		[Ordinal(1)] [RED("radius")] 		public CFloat Radius { get; set;}

		[Ordinal(2)] [RED("clockwiseRotation")] 		public CBool ClockwiseRotation { get; set;}

		[Ordinal(3)] [RED("rotationSpeed")] 		public CFloat RotationSpeed { get; set;}

		[Ordinal(4)] [RED("forwardDirection")] 		public CBool ForwardDirection { get; set;}

		[Ordinal(5)] [RED("transformMatrix")] 		public CMatrix TransformMatrix { get; set;}

		[Ordinal(6)] [RED("localRotation")] 		public EulerAngles LocalRotation { get; set;}

		public W3ElevatorMechanism(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3ElevatorMechanism(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}