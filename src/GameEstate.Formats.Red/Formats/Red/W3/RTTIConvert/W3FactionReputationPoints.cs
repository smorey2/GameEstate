using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3FactionReputationPoints : CObject
	{
		[Ordinal(1)] [RED("currentReputationPoints")] 		public CInt32 CurrentReputationPoints { get; set;}

		[Ordinal(2)] [RED("negativeReputationPoints")] 		public CInt32 NegativeReputationPoints { get; set;}

		public W3FactionReputationPoints(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3FactionReputationPoints(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}