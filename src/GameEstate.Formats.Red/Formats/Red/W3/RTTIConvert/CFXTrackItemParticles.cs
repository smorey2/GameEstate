using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CFXTrackItemParticles : CFXTrackItem
	{
		[Ordinal(1)] [RED("particleSystem")] 		public CSoft<CParticleSystem> ParticleSystem { get; set;}

		[Ordinal(2)] [RED("spawner")] 		public CPtr<IFXSpawner> Spawner { get; set;}

		public CFXTrackItemParticles(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CFXTrackItemParticles(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}