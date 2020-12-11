using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3ConditionalTrigger : CEntity
	{
		[Ordinal(1)] [RED("conditionClass")] 		public CHandle<W3Condition> ConditionClass { get; set;}

		[Ordinal(2)] [RED("effectorClasses", 2,0)] 		public CArray<CHandle<IPerformableAction>> EffectorClasses { get; set;}

		[Ordinal(3)] [RED("affectsPlayer")] 		public CBool AffectsPlayer { get; set;}

		public W3ConditionalTrigger(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3ConditionalTrigger(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}