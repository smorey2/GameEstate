using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CMetalinkComponent : CWayPointComponent
	{
		[Ordinal(1)] [RED("aiAction")] 		public CHandle<IAIExplorationTree> AiAction { get; set;}

		[Ordinal(2)] [RED("pathfindingCostMultiplier")] 		public CFloat PathfindingCostMultiplier { get; set;}

		[Ordinal(3)] [RED("destinationEntityTag")] 		public CName DestinationEntityTag { get; set;}

		[Ordinal(4)] [RED("destinationWaypointComponent")] 		public CString DestinationWaypointComponent { get; set;}

		[Ordinal(5)] [RED("internalObstacleEntity")] 		public EntityHandle InternalObstacleEntity { get; set;}

		[Ordinal(6)] [RED("internalObstacleComponent")] 		public CString InternalObstacleComponent { get; set;}

		[Ordinal(7)] [RED("useInternalObstacle")] 		public CBool UseInternalObstacle { get; set;}

		[Ordinal(8)] [RED("enabledByDefault")] 		public CBool EnabledByDefault { get; set;}

		[Ordinal(9)] [RED("enabled")] 		public CBool Enabled { get; set;}

		[Ordinal(10)] [RED("isGhostLink")] 		public CBool IsGhostLink { get; set;}

		[Ordinal(11)] [RED("questTrackingPortal")] 		public CBool QuestTrackingPortal { get; set;}

		public CMetalinkComponent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CMetalinkComponent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}