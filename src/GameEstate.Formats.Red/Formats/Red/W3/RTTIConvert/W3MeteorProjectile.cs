using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3MeteorProjectile : W3FireballProjectile
	{
		[Ordinal(1)] [RED("explosionRadius")] 		public CFloat ExplosionRadius { get; set;}

		[Ordinal(2)] [RED("markerEntityTemplate")] 		public CHandle<CEntityTemplate> MarkerEntityTemplate { get; set;}

		[Ordinal(3)] [RED("destroyMarkerAfter")] 		public CFloat DestroyMarkerAfter { get; set;}

		[Ordinal(4)] [RED("markerEntity")] 		public CHandle<CEntity> MarkerEntity { get; set;}

		public W3MeteorProjectile(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3MeteorProjectile(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}