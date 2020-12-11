using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3BaitProjectile : W3BoltProjectile
	{
		[Ordinal(1)] [RED("foodSourceToGenerate")] 		public CHandle<CEntityTemplate> FoodSourceToGenerate { get; set;}

		[Ordinal(2)] [RED("addScentToCollidedActors")] 		public CBool AddScentToCollidedActors { get; set;}

		[Ordinal(3)] [RED("attractionDuration")] 		public CFloat AttractionDuration { get; set;}

		[Ordinal(4)] [RED("m_BaitEntity")] 		public CHandle<CEntity> M_BaitEntity { get; set;}

		public W3BaitProjectile(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3BaitProjectile(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}