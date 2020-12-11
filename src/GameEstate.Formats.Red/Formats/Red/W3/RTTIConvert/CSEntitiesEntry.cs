using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CSEntitiesEntry : CVariable
	{
		[Ordinal(1)] [RED("entityTemplate")] 		public CSoft<CEntityTemplate> EntityTemplate { get; set;}

		[Ordinal(2)] [RED("appearances", 2,0)] 		public CArray<CName> Appearances { get; set;}

		[Ordinal(3)] [RED("entitySpawnTags")] 		public TagList EntitySpawnTags { get; set;}

		[Ordinal(4)] [RED("mappinTag")] 		public CName MappinTag { get; set;}

		[Ordinal(5)] [RED("mappinType")] 		public CEnum<ECommMapPinType> MappinType { get; set;}

		[Ordinal(6)] [RED("initializers")] 		public CPtr<CCommunityInitializers> Initializers { get; set;}

		[Ordinal(7)] [RED("guid")] 		public CGUID Guid { get; set;}

		[Ordinal(8)] [RED("despawners")] 		public CPtr<CCommunityInitializers> Despawners { get; set;}

		public CSEntitiesEntry(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CSEntitiesEntry(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}