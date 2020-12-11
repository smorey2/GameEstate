using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3ClueWaypoint : CObject
	{
		[Ordinal(1)] [RED("waypointTag")] 		public CName WaypointTag { get; set;}

		[Ordinal(2)] [RED("clueStateInWaypoint")] 		public CEnum<EBoidClueState> ClueStateInWaypoint { get; set;}

		[Ordinal(3)] [RED("conditionsLogicalOperator")] 		public CEnum<ELogicalOperator> ConditionsLogicalOperator { get; set;}

		[Ordinal(4)] [RED("waypointReachedConditions", 2,0)] 		public CArray<CHandle<W3ClueCondition>> WaypointReachedConditions { get; set;}

		public W3ClueWaypoint(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3ClueWaypoint(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}