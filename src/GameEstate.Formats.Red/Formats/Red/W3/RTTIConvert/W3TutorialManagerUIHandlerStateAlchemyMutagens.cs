using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3TutorialManagerUIHandlerStateAlchemyMutagens : W3TutorialManagerUIHandlerStateTutHandlerBaseState
	{
		[Ordinal(1)] [RED("MUTAGENS")] 		public CName MUTAGENS { get; set;}

		[Ordinal(2)] [RED("currentlySelectedRecipe")] 		public CName CurrentlySelectedRecipe { get; set;}

		[Ordinal(3)] [RED("requiredRecipeName")] 		public CName RequiredRecipeName { get; set;}

		[Ordinal(4)] [RED("selectRecipe")] 		public CName SelectRecipe { get; set;}

		public W3TutorialManagerUIHandlerStateAlchemyMutagens(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3TutorialManagerUIHandlerStateAlchemyMutagens(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}