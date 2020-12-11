using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CStorySceneCutsceneSection : CStorySceneSection
	{
		[Ordinal(1)] [RED("cutscene")] 		public CHandle<CCutsceneTemplate> Cutscene { get; set;}

		[Ordinal(2)] [RED("point")] 		public TagList Point { get; set;}

		[Ordinal(3)] [RED("looped")] 		public CBool Looped { get; set;}

		[Ordinal(4)] [RED("actorOverrides", 2,0)] 		public CArray<SCutsceneActorOverrideMapping> ActorOverrides { get; set;}

		[Ordinal(5)] [RED("clearActorsHands")] 		public CBool ClearActorsHands { get; set;}

		public CStorySceneCutsceneSection(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CStorySceneCutsceneSection(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}