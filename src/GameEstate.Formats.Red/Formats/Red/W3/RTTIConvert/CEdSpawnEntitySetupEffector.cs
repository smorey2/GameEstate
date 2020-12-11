using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CEdSpawnEntitySetupEffector : IEdEntitySetupEffector
	{
		[Ordinal(1)] [RED("template")] 		public CSoft<CEntityTemplate> Template { get; set;}

		[Ordinal(2)] [RED("localPosition")] 		public Vector LocalPosition { get; set;}

		[Ordinal(3)] [RED("localOrientation")] 		public EulerAngles LocalOrientation { get; set;}

		[Ordinal(4)] [RED("detachTemplate")] 		public CBool DetachTemplate { get; set;}

		[Ordinal(5)] [RED("extraTags")] 		public TagList ExtraTags { get; set;}

		public CEdSpawnEntitySetupEffector(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CEdSpawnEntitySetupEffector(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}