using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SVoicesetSlot : CVariable
	{
		[Ordinal(1)] [RED("scene")] 		public CSoft<CStoryScene> Scene { get; set;}

		[Ordinal(2)] [RED("name")] 		public CString Name { get; set;}

		[Ordinal(3)] [RED("voiceTag")] 		public CName VoiceTag { get; set;}

		public SVoicesetSlot(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SVoicesetSlot(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}