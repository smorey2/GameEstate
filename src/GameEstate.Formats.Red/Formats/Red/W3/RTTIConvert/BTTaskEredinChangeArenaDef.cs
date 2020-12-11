using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class BTTaskEredinChangeArenaDef : IBehTreeTaskDefinition
	{
		[Ordinal(1)] [RED("resourceName")] 		public CName ResourceName { get; set;}

		[Ordinal(2)] [RED("eventName")] 		public CName EventName { get; set;}

		[Ordinal(3)] [RED("spawnPortalInTaggedNode")] 		public CBool SpawnPortalInTaggedNode { get; set;}

		[Ordinal(4)] [RED("nodeTag")] 		public CName NodeTag { get; set;}

		[Ordinal(5)] [RED("destinationTag")] 		public CName DestinationTag { get; set;}

		[Ordinal(6)] [RED("factOnPlayerTeleport")] 		public CString FactOnPlayerTeleport { get; set;}

		public BTTaskEredinChangeArenaDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new BTTaskEredinChangeArenaDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}