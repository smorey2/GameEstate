using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAIWanderTree : CAIIdleTree
	{
		[Ordinal(1)] [RED("wanderMoveSpeed")] 		public CFloat WanderMoveSpeed { get; set;}

		[Ordinal(2)] [RED("wanderMoveType")] 		public CEnum<EMoveType> WanderMoveType { get; set;}

		[Ordinal(3)] [RED("wanderMaxDistance")] 		public CFloat WanderMaxDistance { get; set;}

		public CAIWanderTree(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAIWanderTree(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}