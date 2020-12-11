using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CGraphBlock : CObject
	{
		[Ordinal(1)] [RED("sockets", 2,0)] 		public CArray<CPtr<CGraphSocket>> Sockets { get; set;}

		[Ordinal(2)] [RED("position")] 		public Vector Position { get; set;}

		[Ordinal(3)] [RED("version")] 		public CUInt32 Version { get; set;}

		public CGraphBlock(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CGraphBlock(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}