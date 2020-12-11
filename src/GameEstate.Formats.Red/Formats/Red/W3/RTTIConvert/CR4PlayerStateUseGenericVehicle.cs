using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CR4PlayerStateUseGenericVehicle : CPlayerStateUseVehicle
	{
		[Ordinal(1)] [RED("vehicle")] 		public CHandle<CVehicleComponent> Vehicle { get; set;}

		[Ordinal(2)] [RED("camera")] 		public CHandle<CCustomCamera> Camera { get; set;}

		[Ordinal(3)] [RED("signSlotNames", 2,0)] 		public CArray<CName> SignSlotNames { get; set;}

		[Ordinal(4)] [RED("fovVel")] 		public CFloat FovVel { get; set;}

		public CR4PlayerStateUseGenericVehicle(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CR4PlayerStateUseGenericVehicle(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}