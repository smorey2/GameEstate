using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class IMoveSteeringNode : CObject
	{
		[Ordinal(1)] [RED("comment")] 		public CString Comment { get; set;}

		[Ordinal(2)] [RED("graphPosX")] 		public CInt32 GraphPosX { get; set;}

		[Ordinal(3)] [RED("graphPosY")] 		public CInt32 GraphPosY { get; set;}

		[Ordinal(4)] [RED("enabled")] 		public CBool Enabled { get; set;}

		public IMoveSteeringNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new IMoveSteeringNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}