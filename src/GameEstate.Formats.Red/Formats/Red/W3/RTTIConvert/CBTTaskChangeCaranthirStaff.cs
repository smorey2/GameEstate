using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskChangeCaranthirStaff : IBehTreeTask
	{
		[Ordinal(1)] [RED("wasActivated")] 		public CBool WasActivated { get; set;}

		[Ordinal(2)] [RED("startEffect")] 		public CBool StartEffect { get; set;}

		public CBTTaskChangeCaranthirStaff(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskChangeCaranthirStaff(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}