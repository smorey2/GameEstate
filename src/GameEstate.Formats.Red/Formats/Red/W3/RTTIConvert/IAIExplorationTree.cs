using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class IAIExplorationTree : IAITree
	{
		[Ordinal(1)] [RED("interactionPoint")] 		public Vector3 InteractionPoint { get; set;}

		[Ordinal(2)] [RED("destinationPoint")] 		public Vector3 DestinationPoint { get; set;}

		[Ordinal(3)] [RED("metalinkComponent")] 		public CHandle<CComponent> MetalinkComponent { get; set;}

		public IAIExplorationTree(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new IAIExplorationTree(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}