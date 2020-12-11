using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTask3StateAddEffectAttackDef : CBTTask3StateAttackDef
	{
		[Ordinal(1)] [RED("applyEffectInRange")] 		public CFloat ApplyEffectInRange { get; set;}

		[Ordinal(2)] [RED("applyEffectInCone")] 		public CFloat ApplyEffectInCone { get; set;}

		[Ordinal(3)] [RED("applyEffectInterval")] 		public CFloat ApplyEffectInterval { get; set;}

		[Ordinal(4)] [RED("effectType")] 		public CEnum<EEffectType> EffectType { get; set;}

		[Ordinal(5)] [RED("effectDuration")] 		public CFloat EffectDuration { get; set;}

		[Ordinal(6)] [RED("effectValue")] 		public CFloat EffectValue { get; set;}

		[Ordinal(7)] [RED("effectPercentValue")] 		public CFloat EffectPercentValue { get; set;}

		public CBTTask3StateAddEffectAttackDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTask3StateAddEffectAttackDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}