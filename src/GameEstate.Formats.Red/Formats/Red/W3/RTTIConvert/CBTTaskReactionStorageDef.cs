using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskReactionStorageDef : IBehTreeReactionTaskDefinition
	{
		[Ordinal(1)] [RED("onActivate")] 		public CBool OnActivate { get; set;}

		[Ordinal(2)] [RED("onDeactivate")] 		public CBool OnDeactivate { get; set;}

		[Ordinal(3)] [RED("onCompletion")] 		public CBool OnCompletion { get; set;}

		[Ordinal(4)] [RED("setIsAlarmed")] 		public CBool SetIsAlarmed { get; set;}

		[Ordinal(5)] [RED("setTaunted")] 		public CBool SetTaunted { get; set;}

		[Ordinal(6)] [RED("reset")] 		public CBool Reset { get; set;}

		public CBTTaskReactionStorageDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskReactionStorageDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}