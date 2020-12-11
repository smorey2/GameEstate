using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class OrientedBox : CVariable
	{
		[Ordinal(1)] [RED("position")] 		public Vector Position { get; set;}

		[Ordinal(2)] [RED("edge 1")] 		public Vector Edge_1 { get; set;}

		[Ordinal(3)] [RED("edge 2")] 		public Vector Edge_2 { get; set;}

		public OrientedBox(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new OrientedBox(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}