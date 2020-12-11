using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class EulerAnglesSpringDamper : CObject
	{
		[Ordinal(1)] [RED("destValue")] 		public EulerAngles DestValue { get; set;}

		[Ordinal(2)] [RED("currValue")] 		public EulerAngles CurrValue { get; set;}

		[Ordinal(3)] [RED("velValue")] 		public EulerAngles VelValue { get; set;}

		[Ordinal(4)] [RED("smoothTime")] 		public CFloat SmoothTime { get; set;}

		public EulerAnglesSpringDamper(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new EulerAnglesSpringDamper(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}