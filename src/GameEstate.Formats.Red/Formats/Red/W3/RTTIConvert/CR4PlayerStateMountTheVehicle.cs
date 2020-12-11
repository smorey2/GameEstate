using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CR4PlayerStateMountTheVehicle : CPlayerStateBase
	{
		[Ordinal(1)] [RED("vehicle")] 		public CHandle<CVehicleComponent> Vehicle { get; set;}

		[Ordinal(2)] [RED("mountType")] 		public CEnum<EMountType> MountType { get; set;}

		[Ordinal(3)] [RED("vehicleSlot")] 		public CEnum<EVehicleSlot> VehicleSlot { get; set;}

		[Ordinal(4)] [RED("camera")] 		public CHandle<CCustomCamera> Camera { get; set;}

		public CR4PlayerStateMountTheVehicle(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CR4PlayerStateMountTheVehicle(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}