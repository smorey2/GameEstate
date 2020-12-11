using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SBoatDestructionVolume : CVariable
	{
		[Ordinal(1)] [RED("areaHealth")] 		public CFloat AreaHealth { get; set;}

		[Ordinal(2)] [RED("volumeCorners")] 		public Vector VolumeCorners { get; set;}

		[Ordinal(3)] [RED("volumeLocalPosition")] 		public Vector VolumeLocalPosition { get; set;}

		public SBoatDestructionVolume(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SBoatDestructionVolume(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}