using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CR4OverlayMenu : CR4MenuBase
	{
		[Ordinal(1)] [RED("m_BlurBackground")] 		public CBool M_BlurBackground { get; set;}

		[Ordinal(2)] [RED("m_PauseGame")] 		public CBool M_PauseGame { get; set;}

		public CR4OverlayMenu(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CR4OverlayMenu(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}