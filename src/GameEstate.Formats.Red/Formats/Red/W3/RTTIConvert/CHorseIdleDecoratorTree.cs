using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CHorseIdleDecoratorTree : CAIMonsterIdleDecorator
	{
		[Ordinal(1)] [RED("actionPointSelector")] 		public CHandle<CHorseParkingActionPointSelector> ActionPointSelector { get; set;}

		[Ordinal(2)] [RED("packName")] 		public CName PackName { get; set;}

		public CHorseIdleDecoratorTree(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CHorseIdleDecoratorTree(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}