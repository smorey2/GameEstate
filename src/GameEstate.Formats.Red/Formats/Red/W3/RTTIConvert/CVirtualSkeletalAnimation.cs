using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CVirtualSkeletalAnimation : CSkeletalAnimation
	{
		[Ordinal(1)] [RED("virtualAnimations", 2,0)] 		public CArray<VirtualAnimation> VirtualAnimations { get; set;}

		[Ordinal(2)] [RED("virtualAnimationsOverride", 2,0)] 		public CArray<VirtualAnimation> VirtualAnimationsOverride { get; set;}

		[Ordinal(3)] [RED("virtualAnimationsAdditive", 2,0)] 		public CArray<VirtualAnimation> VirtualAnimationsAdditive { get; set;}

		[Ordinal(4)] [RED("virtualMotions", 2,0)] 		public CArray<VirtualAnimationMotion> VirtualMotions { get; set;}

		[Ordinal(5)] [RED("virtualFKs", 2,0)] 		public CArray<VirtualAnimationPoseFK> VirtualFKs { get; set;}

		[Ordinal(6)] [RED("virtualIKs", 2,0)] 		public CArray<VirtualAnimationPoseIK> VirtualIKs { get; set;}

		public CVirtualSkeletalAnimation(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CVirtualSkeletalAnimation(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}