using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3FocusModeEffectIntensity : CObject
	{
		[Ordinal(1)] [RED("chooseEntityStrategy")] 		public CEnum<EFocusModeChooseEntityStrategy> ChooseEntityStrategy { get; set;}

		[Ordinal(2)] [RED("bestEntity")] 		public CHandle<CEntity> BestEntity { get; set;}

		[Ordinal(3)] [RED("bestDistance")] 		public CFloat BestDistance { get; set;}

		[Ordinal(4)] [RED("lastDistance")] 		public CFloat LastDistance { get; set;}

		[Ordinal(5)] [RED("bestIntensity")] 		public CFloat BestIntensity { get; set;}

		[Ordinal(6)] [RED("lastIntensity")] 		public CFloat LastIntensity { get; set;}

		public W3FocusModeEffectIntensity(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3FocusModeEffectIntensity(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}