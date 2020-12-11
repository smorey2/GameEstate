using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskTornadoAttack : CBTTaskAttack
	{
		[Ordinal(1)] [RED("minCameraShakeStrength")] 		public CFloat MinCameraShakeStrength { get; set;}

		[Ordinal(2)] [RED("maxCameraShakeStrength")] 		public CFloat MaxCameraShakeStrength { get; set;}

		[Ordinal(3)] [RED("cameraShakeRange")] 		public CFloat CameraShakeRange { get; set;}

		[Ordinal(4)] [RED("cameraShakeInterval")] 		public CFloat CameraShakeInterval { get; set;}

		[Ordinal(5)] [RED("victimTestInterval")] 		public CFloat VictimTestInterval { get; set;}

		[Ordinal(6)] [RED("debuffInterval")] 		public CFloat DebuffInterval { get; set;}

		[Ordinal(7)] [RED("damageInterval")] 		public CFloat DamageInterval { get; set;}

		[Ordinal(8)] [RED("damageMultiplier")] 		public CFloat DamageMultiplier { get; set;}

		[Ordinal(9)] [RED("affectEnemiesInRangeMin")] 		public CFloat AffectEnemiesInRangeMin { get; set;}

		[Ordinal(10)] [RED("affectEnemiesInRangeMax")] 		public CFloat AffectEnemiesInRangeMax { get; set;}

		[Ordinal(11)] [RED("castingLoopTime")] 		public CFloat CastingLoopTime { get; set;}

		[Ordinal(12)] [RED("setBehVarOnDeactivation")] 		public CName SetBehVarOnDeactivation { get; set;}

		[Ordinal(13)] [RED("setBehVarValueOnDeactivation")] 		public CFloat SetBehVarValueOnDeactivation { get; set;}

		[Ordinal(14)] [RED("debuffTypeInRangeMin")] 		public CEnum<EEffectType> DebuffTypeInRangeMin { get; set;}

		[Ordinal(15)] [RED("rotateToNodeByTagOnDebuffMin")] 		public CName RotateToNodeByTagOnDebuffMin { get; set;}

		[Ordinal(16)] [RED("debuffTypeInRangeMax")] 		public CEnum<EEffectType> DebuffTypeInRangeMax { get; set;}

		[Ordinal(17)] [RED("debuffDurationInRangeMin")] 		public CFloat DebuffDurationInRangeMin { get; set;}

		[Ordinal(18)] [RED("debuffDurationInRangeMax")] 		public CFloat DebuffDurationInRangeMax { get; set;}

		[Ordinal(19)] [RED("activateOnAnimEvent")] 		public CName ActivateOnAnimEvent { get; set;}

		[Ordinal(20)] [RED("additionalFxOnDamageVictim")] 		public CName AdditionalFxOnDamageVictim { get; set;}

		[Ordinal(21)] [RED("m_activated")] 		public CBool M_activated { get; set;}

		public CBTTaskTornadoAttack(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskTornadoAttack(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}