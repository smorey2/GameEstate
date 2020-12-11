using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class Rect : CVariable
	{
		[Ordinal(1)] [RED("m_left")] 		public CInt32 M_left { get; set;}

		[Ordinal(2)] [RED("m_top")] 		public CInt32 M_top { get; set;}

		[Ordinal(3)] [RED("m_right")] 		public CInt32 M_right { get; set;}

		[Ordinal(4)] [RED("m_bottom")] 		public CInt32 M_bottom { get; set;}

		public Rect(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new Rect(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}