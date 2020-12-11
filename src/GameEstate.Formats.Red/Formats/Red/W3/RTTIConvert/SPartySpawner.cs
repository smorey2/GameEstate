using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SPartySpawner : CVariable
	{
		[Ordinal(1)] [RED("firstIndex")] 		public CUInt32 FirstIndex { get; set;}

		[Ordinal(2)] [RED("waypointsCount")] 		public CUInt32 WaypointsCount { get; set;}

		[Ordinal(3)] [RED("mappingIndex")] 		public CUInt32 MappingIndex { get; set;}

		public SPartySpawner(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SPartySpawner(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}