using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CTeleportDetectorData : CObject
	{
		[Ordinal(1)] [RED("angleDif")] 		public CFloat AngleDif { get; set;}

		[Ordinal(2)] [RED("pelvisPositionThreshold")] 		public CFloat PelvisPositionThreshold { get; set;}

		[Ordinal(3)] [RED("pelvisTeleportData")] 		public STeleportBone PelvisTeleportData { get; set;}

		[Ordinal(4)] [RED("teleportedBones", 2,0)] 		public CArray<STeleportBone> TeleportedBones { get; set;}

		public CTeleportDetectorData(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CTeleportDetectorData(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}