using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehTreeDecoratorRidingManagerDefinition : IBehTreeNodeSpecialDefinition
	{
		[Ordinal(1)] [RED("child")] 		public CPtr<IBehTreeNodeDefinition> Child { get; set;}

		[Ordinal(2)] [RED("mountHorseChild")] 		public CPtr<IBehTreeNodeDefinition> MountHorseChild { get; set;}

		[Ordinal(3)] [RED("dismountHorseChild")] 		public CPtr<IBehTreeNodeDefinition> DismountHorseChild { get; set;}

		[Ordinal(4)] [RED("mountBoatChild")] 		public CPtr<IBehTreeNodeDefinition> MountBoatChild { get; set;}

		[Ordinal(5)] [RED("dismountBoatChild")] 		public CPtr<IBehTreeNodeDefinition> DismountBoatChild { get; set;}

		public CBehTreeDecoratorRidingManagerDefinition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehTreeDecoratorRidingManagerDefinition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}