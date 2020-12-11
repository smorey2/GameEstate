using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SPlaneMovementParameters : CVariable
	{
		[Ordinal(1)] [RED("m_SpeedMaxF")] 		public CFloat M_SpeedMaxF { get; set;}

		[Ordinal(2)] [RED("m_AccelF")] 		public CFloat M_AccelF { get; set;}

		[Ordinal(3)] [RED("m_DecelF")] 		public CFloat M_DecelF { get; set;}

		[Ordinal(4)] [RED("m_BrakeF")] 		public CFloat M_BrakeF { get; set;}

		[Ordinal(5)] [RED("m_BrakeDotF")] 		public CFloat M_BrakeDotF { get; set;}

		public SPlaneMovementParameters(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SPlaneMovementParameters(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}