using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CGraphConnection : ISerializable
	{
		[Ordinal(1)] [RED("source")] 		public CPtr<CGraphSocket> Source { get; set;}

		[Ordinal(2)] [RED("destination")] 		public CPtr<CGraphSocket> Destination { get; set;}

		[Ordinal(3)] [RED("inactive")] 		public CBool Inactive { get; set;}

		public CGraphConnection(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CGraphConnection(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}