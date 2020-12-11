using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3AirDrainEntity : CGameplayEntity
	{
		[Ordinal(1)] [RED("customDrainPoints")] 		public CFloat CustomDrainPoints { get; set;}

		[Ordinal(2)] [RED("customDrainPercents")] 		public CFloat CustomDrainPercents { get; set;}

		[Ordinal(3)] [RED("factOnActivated")] 		public CString FactOnActivated { get; set;}

		[Ordinal(4)] [RED("factOnDeactivated")] 		public CString FactOnDeactivated { get; set;}

		public W3AirDrainEntity(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3AirDrainEntity(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}