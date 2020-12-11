using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskDespawn : IBehTreeTask
	{
		[Ordinal(1)] [RED("callFromQuest")] 		public CBool CallFromQuest { get; set;}

		[Ordinal(2)] [RED("destroyCooldown")] 		public CFloat DestroyCooldown { get; set;}

		[Ordinal(3)] [RED("despawn")] 		public CBool Despawn { get; set;}

		[Ordinal(4)] [RED("disappearfxName")] 		public CName DisappearfxName { get; set;}

		[Ordinal(5)] [RED("emptyName")] 		public CName EmptyName { get; set;}

		[Ordinal(6)] [RED("despawnEventName")] 		public CName DespawnEventName { get; set;}

		[Ordinal(7)] [RED("raiseEventName")] 		public CName RaiseEventName { get; set;}

		public CBTTaskDespawn(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskDespawn(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}