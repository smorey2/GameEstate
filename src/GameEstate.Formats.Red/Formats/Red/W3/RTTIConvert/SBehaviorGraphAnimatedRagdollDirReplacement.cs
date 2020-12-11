using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SBehaviorGraphAnimatedRagdollDirReplacement : CVariable
	{
		[Ordinal(1)] [RED("probability")] 		public CFloat Probability { get; set;}

		[Ordinal(2)] [RED("index")] 		public CUInt32 Index { get; set;}

		public SBehaviorGraphAnimatedRagdollDirReplacement(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SBehaviorGraphAnimatedRagdollDirReplacement(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}