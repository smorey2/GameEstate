using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CMoveSTArrive : IMoveSteeringTask
	{
		[Ordinal(1)] [RED("rotationVar")] 		public CName RotationVar { get; set;}

		[Ordinal(2)] [RED("rotationEvent")] 		public CName RotationEvent { get; set;}

		[Ordinal(3)] [RED("rotationNotification")] 		public CName RotationNotification { get; set;}

		public CMoveSTArrive(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CMoveSTArrive(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}