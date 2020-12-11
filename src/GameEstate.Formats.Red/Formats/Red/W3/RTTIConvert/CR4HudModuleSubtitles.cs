using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CR4HudModuleSubtitles : CR4HudModuleBase
	{
		[Ordinal(1)] [RED("m_fxAddSubtitleSFF")] 		public CHandle<CScriptedFlashFunction> M_fxAddSubtitleSFF { get; set;}

		[Ordinal(2)] [RED("m_fxRemoveSubtitleSFF")] 		public CHandle<CScriptedFlashFunction> M_fxRemoveSubtitleSFF { get; set;}

		[Ordinal(3)] [RED("m_fxUpdateWidthSFF")] 		public CHandle<CScriptedFlashFunction> M_fxUpdateWidthSFF { get; set;}

		public CR4HudModuleSubtitles(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CR4HudModuleSubtitles(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}