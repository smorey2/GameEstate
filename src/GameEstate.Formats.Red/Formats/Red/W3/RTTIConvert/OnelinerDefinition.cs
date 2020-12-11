using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class OnelinerDefinition : CVariable
	{
		[Ordinal(1)] [RED("m_Target")] 		public CHandle<CActor> M_Target { get; set;}

		[Ordinal(2)] [RED("m_Text")] 		public CString M_Text { get; set;}

		[Ordinal(3)] [RED("m_ID")] 		public CInt32 M_ID { get; set;}

		public OnelinerDefinition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new OnelinerDefinition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}