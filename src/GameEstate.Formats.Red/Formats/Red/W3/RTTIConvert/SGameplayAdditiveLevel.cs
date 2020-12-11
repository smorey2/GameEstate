using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SGameplayAdditiveLevel : CVariable
	{
		[Ordinal(1)] [RED("useLevel")] 		public CBool UseLevel { get; set;}

		[Ordinal(2)] [RED("synchronize")] 		public CBool Synchronize { get; set;}

		[Ordinal(3)] [RED("animations", 2,0)] 		public CArray<SGameplayAdditiveAnimation> Animations { get; set;}

		public SGameplayAdditiveLevel(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SGameplayAdditiveLevel(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}