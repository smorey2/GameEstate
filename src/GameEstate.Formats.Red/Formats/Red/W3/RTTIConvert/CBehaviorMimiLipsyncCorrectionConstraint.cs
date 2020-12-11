using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorMimiLipsyncCorrectionConstraint : IBehaviorMimicConstraint
	{
		[Ordinal(1)] [RED("controlTrack")] 		public CInt32 ControlTrack { get; set;}

		[Ordinal(2)] [RED("trackBegin")] 		public CInt32 TrackBegin { get; set;}

		[Ordinal(3)] [RED("trackEnd")] 		public CInt32 TrackEnd { get; set;}

		public CBehaviorMimiLipsyncCorrectionConstraint(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorMimiLipsyncCorrectionConstraint(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}