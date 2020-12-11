using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class STimeScaleSourceSet : CVariable
	{
		[Ordinal(1)] [RED("priority")] 		public CInt32 Priority { get; set;}

		[Ordinal(2)] [RED("entries", 2,0)] 		public CArray<STimeScaleSource> Entries { get; set;}

		public STimeScaleSourceSet(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new STimeScaleSourceSet(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}