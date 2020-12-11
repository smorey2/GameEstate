using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CMoveSTForwardCollisionResponse : CMoveSTCollisionResponse
	{
		[Ordinal(1)] [RED("probeDistanceInTime")] 		public CFloat ProbeDistanceInTime { get; set;}

		[Ordinal(2)] [RED("crowdThroughVar")] 		public CName CrowdThroughVar { get; set;}

		public CMoveSTForwardCollisionResponse(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CMoveSTForwardCollisionResponse(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}