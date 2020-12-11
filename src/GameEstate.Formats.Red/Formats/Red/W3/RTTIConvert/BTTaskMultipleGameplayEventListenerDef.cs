using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class BTTaskMultipleGameplayEventListenerDef : IBehTreeTaskDefinition
	{
		[Ordinal(1)] [RED("gameplayEventsArray", 2,0)] 		public CArray<CName> GameplayEventsArray { get; set;}

		[Ordinal(2)] [RED("validFor")] 		public CFloat ValidFor { get; set;}

		[Ordinal(3)] [RED("activeFor")] 		public CFloat ActiveFor { get; set;}

		public BTTaskMultipleGameplayEventListenerDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new BTTaskMultipleGameplayEventListenerDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}