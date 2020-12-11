using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskScaredWhileSitting : CBTTaskShouldBeScaredOnOverlay
	{
		[Ordinal(1)] [RED("leftItem")] 		public CHandle<CDrawableComponent> LeftItem { get; set;}

		[Ordinal(2)] [RED("rightItem")] 		public CHandle<CDrawableComponent> RightItem { get; set;}

		[Ordinal(3)] [RED("entity")] 		public CHandle<CEntity> Entity { get; set;}

		public CBTTaskScaredWhileSitting(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskScaredWhileSitting(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}