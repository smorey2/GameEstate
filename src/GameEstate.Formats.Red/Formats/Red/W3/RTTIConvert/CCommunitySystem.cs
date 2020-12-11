using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CCommunitySystem : IGameSystem
	{
		[Ordinal(1)] [RED("apMan")] 		public CHandle<CActionPointManager> ApMan { get; set;}

		[Ordinal(2)] [RED("communitySpawnInitializer")] 		public CHandle<ISpawnTreeInitializerAI> CommunitySpawnInitializer { get; set;}

		[Ordinal(3)] [RED("wmkMapMenu")] 		public CHandle<WmkMapMenu> WmkMapMenu { get; set;}

		public CCommunitySystem(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CCommunitySystem(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}