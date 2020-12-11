using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAINpcReactionStopAndComment : CAINpcReaction
	{
		[Ordinal(1)] [RED("stopDuration")] 		public CFloat StopDuration { get; set;}

		[Ordinal(2)] [RED("activationChance")] 		public CInt32 ActivationChance { get; set;}

		[Ordinal(3)] [RED("distanceToInterrupt")] 		public CInt32 DistanceToInterrupt { get; set;}

		public CAINpcReactionStopAndComment(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAINpcReactionStopAndComment(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}