using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CExplorationComponent : CComponent
	{
		[Ordinal(1)] [RED("explorationId")] 		public CEnum<EExplorationType> ExplorationId { get; set;}

		[Ordinal(2)] [RED("start")] 		public Vector Start { get; set;}

		[Ordinal(3)] [RED("end")] 		public Vector End { get; set;}

		[Ordinal(4)] [RED("componentForEvents")] 		public CString ComponentForEvents { get; set;}

		[Ordinal(5)] [RED("internalExploration")] 		public CBool InternalExploration { get; set;}

		public CExplorationComponent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CExplorationComponent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}