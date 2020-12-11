using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CParticleDrawerBeam : IParticleDrawer
	{
		[Ordinal(1)] [RED("texturesPerUnit")] 		public CFloat TexturesPerUnit { get; set;}

		[Ordinal(2)] [RED("spread")] 		public CPtr<IEvaluatorVector> Spread { get; set;}

		[Ordinal(3)] [RED("numSegments")] 		public CUInt32 NumSegments { get; set;}

		public CParticleDrawerBeam(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CParticleDrawerBeam(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}