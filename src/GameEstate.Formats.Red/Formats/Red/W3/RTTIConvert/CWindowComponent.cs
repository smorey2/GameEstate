using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CWindowComponent : CMeshComponent
	{
		[Ordinal(1)] [RED("startEmissiveHour")] 		public CFloat StartEmissiveHour { get; set;}

		[Ordinal(2)] [RED("startEmissiveFadeTime")] 		public CFloat StartEmissiveFadeTime { get; set;}

		[Ordinal(3)] [RED("endEmissiveHour")] 		public CFloat EndEmissiveHour { get; set;}

		[Ordinal(4)] [RED("endEmissiveFadeTime")] 		public CFloat EndEmissiveFadeTime { get; set;}

		[Ordinal(5)] [RED("randomRange")] 		public CFloat RandomRange { get; set;}

		public CWindowComponent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CWindowComponent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}