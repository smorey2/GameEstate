using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAnimDangleConstraint_NobleDressFix : IAnimDangleConstraint
	{
		[Ordinal(1)] [RED("boneNameA")] 		public CString BoneNameA { get; set;}

		[Ordinal(2)] [RED("boneNameB")] 		public CString BoneNameB { get; set;}

		[Ordinal(3)] [RED("boneAxisA")] 		public CEnum<EAxis> BoneAxisA { get; set;}

		[Ordinal(4)] [RED("boneAxisB")] 		public CEnum<EAxis> BoneAxisB { get; set;}

		[Ordinal(5)] [RED("boneValueA")] 		public CFloat BoneValueA { get; set;}

		[Ordinal(6)] [RED("boneValueB")] 		public CFloat BoneValueB { get; set;}

		public CAnimDangleConstraint_NobleDressFix(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAnimDangleConstraint_NobleDressFix(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}