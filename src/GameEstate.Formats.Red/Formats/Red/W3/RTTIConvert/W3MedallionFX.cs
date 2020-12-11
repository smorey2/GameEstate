using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3MedallionFX : CEntity
	{
		[Ordinal(1)] [RED("scaleVector")] 		public Vector ScaleVector { get; set;}

		[Ordinal(2)] [RED("medallionScaleRate")] 		public CFloat MedallionScaleRate { get; set;}

		[Ordinal(3)] [RED("effectDuration")] 		public CFloat EffectDuration { get; set;}

		[Ordinal(4)] [RED("medallionComponent")] 		public CHandle<CComponent> MedallionComponent { get; set;}

		public W3MedallionFX(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3MedallionFX(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}