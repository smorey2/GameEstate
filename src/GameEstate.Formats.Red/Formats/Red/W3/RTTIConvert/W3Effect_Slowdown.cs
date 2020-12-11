using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3Effect_Slowdown : CBaseGameplayEffect
	{
		[Ordinal(1)] [RED("slowdownCauserId")] 		public CInt32 SlowdownCauserId { get; set;}

		[Ordinal(2)] [RED("decayPerSec")] 		public CFloat DecayPerSec { get; set;}

		[Ordinal(3)] [RED("decayDelay")] 		public CFloat DecayDelay { get; set;}

		[Ordinal(4)] [RED("delayTimer")] 		public CFloat DelayTimer { get; set;}

		[Ordinal(5)] [RED("slowdown")] 		public CFloat Slowdown { get; set;}

		public W3Effect_Slowdown(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3Effect_Slowdown(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}