using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CActionPoint : CGameplayEntity
	{
		[Ordinal(1)] [RED("events", 2,0)] 		public CArray<SEntityActionsRouterEntry> Events { get; set;}

		[Ordinal(2)] [RED("actionBreakable")] 		public CBool ActionBreakable { get; set;}

		[Ordinal(3)] [RED("overrideActionBreakableInComponent")] 		public CBool OverrideActionBreakableInComponent { get; set;}

		public CActionPoint(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CActionPoint(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}