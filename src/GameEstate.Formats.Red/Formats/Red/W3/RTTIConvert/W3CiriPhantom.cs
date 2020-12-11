using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3CiriPhantom : CGameplayEntity
	{
		[Ordinal(1)] [RED("owner")] 		public CHandle<CActor> Owner { get; set;}

		[Ordinal(2)] [RED("target")] 		public CHandle<CActor> Target { get; set;}

		[Ordinal(3)] [RED("rotationDamper")] 		public CHandle<EulerAnglesSpringDamper> RotationDamper { get; set;}

		public W3CiriPhantom(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3CiriPhantom(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}