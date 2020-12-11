using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CFormation : CResource
	{
		[Ordinal(1)] [RED("uniqueFormationName")] 		public CName UniqueFormationName { get; set;}

		[Ordinal(2)] [RED("formationLogic")] 		public CPtr<IFormationLogic> FormationLogic { get; set;}

		[Ordinal(3)] [RED("steeringGraph")] 		public CHandle<CMoveSteeringBehavior> SteeringGraph { get; set;}

		public CFormation(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CFormation(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}