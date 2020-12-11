using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class NavigationCorrection : CObject
	{
		[Ordinal(1)] [RED("corrected")] 		public CBool Corrected { get; set;}

		[Ordinal(2)] [RED("direction")] 		public Vector Direction { get; set;}

		[Ordinal(3)] [RED("angle")] 		public CFloat Angle { get; set;}

		[Ordinal(4)] [RED("type")] 		public CEnum<EMovementCorrectionType> Type { get; set;}

		[Ordinal(5)] [RED("color")] 		public CColor Color { get; set;}

		public NavigationCorrection(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new NavigationCorrection(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}