using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskPlayScreamSound : IBehTreeTask
	{
		[Ordinal(1)] [RED("minFrequency")] 		public CFloat MinFrequency { get; set;}

		[Ordinal(2)] [RED("maxFrequency")] 		public CFloat MaxFrequency { get; set;}

		[Ordinal(3)] [RED("actor")] 		public CHandle<CActor> Actor { get; set;}

		public CBTTaskPlayScreamSound(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskPlayScreamSound(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}