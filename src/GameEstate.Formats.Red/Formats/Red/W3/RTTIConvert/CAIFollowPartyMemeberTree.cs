using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAIFollowPartyMemeberTree : CAIIdleTree
	{
		[Ordinal(1)] [RED("followPartyMember")] 		public CName FollowPartyMember { get; set;}

		[Ordinal(2)] [RED("followDistance")] 		public CFloat FollowDistance { get; set;}

		[Ordinal(3)] [RED("moveType")] 		public CEnum<EMoveType> MoveType { get; set;}

		public CAIFollowPartyMemeberTree(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAIFollowPartyMemeberTree(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}