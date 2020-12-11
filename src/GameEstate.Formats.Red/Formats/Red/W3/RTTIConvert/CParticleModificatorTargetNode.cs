using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CParticleModificatorTargetNode : IParticleModificator
	{
		[Ordinal(1)] [RED("forceScale")] 		public CPtr<IEvaluatorFloat> ForceScale { get; set;}

		[Ordinal(2)] [RED("killRadius")] 		public CPtr<IEvaluatorFloat> KillRadius { get; set;}

		[Ordinal(3)] [RED("maxForce")] 		public CFloat MaxForce { get; set;}

		public CParticleModificatorTargetNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CParticleModificatorTargetNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}