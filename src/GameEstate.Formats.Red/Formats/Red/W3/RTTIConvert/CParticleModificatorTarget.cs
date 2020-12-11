using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CParticleModificatorTarget : IParticleModificator
	{
		[Ordinal(1)] [RED("position")] 		public CPtr<IEvaluatorVector> Position { get; set;}

		[Ordinal(2)] [RED("forceScale")] 		public CPtr<IEvaluatorFloat> ForceScale { get; set;}

		[Ordinal(3)] [RED("killRadius")] 		public CPtr<IEvaluatorFloat> KillRadius { get; set;}

		[Ordinal(4)] [RED("maxForce")] 		public CFloat MaxForce { get; set;}

		public CParticleModificatorTarget(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CParticleModificatorTarget(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}