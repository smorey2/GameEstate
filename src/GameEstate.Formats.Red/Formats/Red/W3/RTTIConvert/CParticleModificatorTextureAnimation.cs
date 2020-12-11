using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CParticleModificatorTextureAnimation : IParticleModificator
	{
		[Ordinal(1)] [RED("initialFrame")] 		public CPtr<IEvaluatorFloat> InitialFrame { get; set;}

		[Ordinal(2)] [RED("animationSpeed")] 		public CPtr<IEvaluatorFloat> AnimationSpeed { get; set;}

		[Ordinal(3)] [RED("animationMode")] 		public CEnum<ETextureAnimationMode> AnimationMode { get; set;}

		public CParticleModificatorTextureAnimation(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CParticleModificatorTextureAnimation(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}