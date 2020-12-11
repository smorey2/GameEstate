using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SPartyWaypointData : CVariable
	{
		[Ordinal(1)] [RED("position")] 		public Vector3 Position { get; set;}

		[Ordinal(2)] [RED("rotation")] 		public CFloat Rotation { get; set;}

		[Ordinal(3)] [RED("memberName")] 		public CName MemberName { get; set;}

		public SPartyWaypointData(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SPartyWaypointData(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}