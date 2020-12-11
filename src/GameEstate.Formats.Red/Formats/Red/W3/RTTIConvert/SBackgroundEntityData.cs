using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SBackgroundEntityData : CVariable
	{
		[Ordinal(1)] [RED("entityTemplate")] 		public CHandle<CEntityTemplate> EntityTemplate { get; set;}

		[Ordinal(2)] [RED("spawnSlotName")] 		public CName SpawnSlotName { get; set;}

		[Ordinal(3)] [RED("workAnimationEvent")] 		public CEnum<EBackgroundNPCWork_Single> WorkAnimationEvent { get; set;}

		[Ordinal(4)] [RED("appearanceName")] 		public CName AppearanceName { get; set;}

		public SBackgroundEntityData(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SBackgroundEntityData(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}