using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskAddEffectToTargetDef : IBehTreeTaskDefinition
	{
		[Ordinal(1)] [RED("onActivate")] 		public CBool OnActivate { get; set;}

		[Ordinal(2)] [RED("onEvent")] 		public CBool OnEvent { get; set;}

		[Ordinal(3)] [RED("onDeactivate")] 		public CBool OnDeactivate { get; set;}

		[Ordinal(4)] [RED("useLookAt")] 		public CBool UseLookAt { get; set;}

		[Ordinal(5)] [RED("applyEffectInterval")] 		public CFloat ApplyEffectInterval { get; set;}

		[Ordinal(6)] [RED("applyEffectForTime")] 		public CFloat ApplyEffectForTime { get; set;}

		[Ordinal(7)] [RED("applyEffectInRange")] 		public CFloat ApplyEffectInRange { get; set;}

		[Ordinal(8)] [RED("applyEffectInCone")] 		public CFloat ApplyEffectInCone { get; set;}

		[Ordinal(9)] [RED("eventName")] 		public CName EventName { get; set;}

		[Ordinal(10)] [RED("effectType")] 		public CEnum<EEffectType> EffectType { get; set;}

		[Ordinal(11)] [RED("effectDuration")] 		public CFloat EffectDuration { get; set;}

		[Ordinal(12)] [RED("effectValue")] 		public CFloat EffectValue { get; set;}

		[Ordinal(13)] [RED("effectValuePerc")] 		public CFloat EffectValuePerc { get; set;}

		[Ordinal(14)] [RED("applyOnOwner")] 		public CBool ApplyOnOwner { get; set;}

		[Ordinal(15)] [RED("customFXName")] 		public CName CustomFXName { get; set;}

		[Ordinal(16)] [RED("breakQuen")] 		public CBool BreakQuen { get; set;}

		public CBTTaskAddEffectToTargetDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskAddEffectToTargetDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}