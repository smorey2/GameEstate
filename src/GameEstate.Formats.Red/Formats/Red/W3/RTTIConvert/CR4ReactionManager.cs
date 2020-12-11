using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CR4ReactionManager : CBehTreeReactionManager
	{
		[Ordinal(1)] [RED("rainReactionsEnabled")] 		public CBool RainReactionsEnabled { get; set;}

		[Ordinal(2)] [RED("rainEventParams")] 		public CHandle<CBehTreeReactionEventData> RainEventParams { get; set;}

		public CR4ReactionManager(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CR4ReactionManager(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}