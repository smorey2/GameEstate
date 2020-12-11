using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3NPCBackgroundNew : CEntity
	{
		[Ordinal(1)] [RED("behaviorWorkNumber")] 		public CInt32 BehaviorWorkNumber { get; set;}

		[Ordinal(2)] [RED("randomized")] 		public CBool Randomized { get; set;}

		[Ordinal(3)] [RED("maxWorkNumber")] 		public CInt32 MaxWorkNumber { get; set;}

		[Ordinal(4)] [RED("excludeIdle")] 		public CBool ExcludeIdle { get; set;}

		public W3NPCBackgroundNew(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3NPCBackgroundNew(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}