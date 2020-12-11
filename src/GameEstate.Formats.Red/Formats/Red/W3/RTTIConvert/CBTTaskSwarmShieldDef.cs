using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskSwarmShieldDef : CBTTaskPlayAnimationEventDecoratorDef
	{
		[Ordinal(1)] [RED("stabilizationTimer")] 		public CFloat StabilizationTimer { get; set;}

		[Ordinal(2)] [RED("disperse")] 		public CBool Disperse { get; set;}

		public CBTTaskSwarmShieldDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskSwarmShieldDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}