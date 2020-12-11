using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAIReaction : CObject
	{
		[Ordinal(1)] [RED("fieldName")] 		public CName FieldName { get; set;}

		[Ordinal(2)] [RED("cooldownTime")] 		public CFloat CooldownTime { get; set;}

		[Ordinal(3)] [RED("visibilityTest")] 		public CEnum<EVisibilityTest> VisibilityTest { get; set;}

		[Ordinal(4)] [RED("range")] 		public SAIReactionRange Range { get; set;}

		[Ordinal(5)] [RED("factTest")] 		public SAIReactionFactTest FactTest { get; set;}

		[Ordinal(6)] [RED("condition")] 		public CPtr<IReactionCondition> Condition { get; set;}

		[Ordinal(7)] [RED("action")] 		public CPtr<IReactionAction> Action { get; set;}

		public CAIReaction(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAIReaction(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}