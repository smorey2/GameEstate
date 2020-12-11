using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CMaterialBlockShiftColorRB : CMaterialBlock
	{
		[Ordinal(1)] [RED("colorThresholdLow")] 		public CFloat ColorThresholdLow { get; set;}

		[Ordinal(2)] [RED("colorThresholdHigh")] 		public CFloat ColorThresholdHigh { get; set;}

		[Ordinal(3)] [RED("satThresholdLow")] 		public CFloat SatThresholdLow { get; set;}

		[Ordinal(4)] [RED("satThresholdHigh")] 		public CFloat SatThresholdHigh { get; set;}

		public CMaterialBlockShiftColorRB(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CMaterialBlockShiftColorRB(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}