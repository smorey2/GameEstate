using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CSequentialAnimationController : IAnimationController
	{
		[Ordinal(1)] [RED("animations", 2,0)] 		public CArray<CName> Animations { get; set;}

		[Ordinal(2)] [RED("speeds", 2,0)] 		public CArray<CFloat> Speeds { get; set;}

		[Ordinal(3)] [RED("startingOffsetRange")] 		public CFloat StartingOffsetRange { get; set;}

		[Ordinal(4)] [RED("startingOffsetBias")] 		public CFloat StartingOffsetBias { get; set;}

		public CSequentialAnimationController(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CSequentialAnimationController(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}