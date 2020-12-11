using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CR4StartScreenMenuBase : CR4MenuBase
	{
		[Ordinal(1)] [RED("_fadeDuration")] 		public CFloat _fadeDuration { get; set;}

		[Ordinal(2)] [RED("m_fxSetFadeDuration")] 		public CHandle<CScriptedFlashFunction> M_fxSetFadeDuration { get; set;}

		[Ordinal(3)] [RED("m_fxSetIsStageDemo")] 		public CHandle<CScriptedFlashFunction> M_fxSetIsStageDemo { get; set;}

		[Ordinal(4)] [RED("m_fxStartFade")] 		public CHandle<CScriptedFlashFunction> M_fxStartFade { get; set;}

		[Ordinal(5)] [RED("m_fxSetGameLogoLanguage")] 		public CHandle<CScriptedFlashFunction> M_fxSetGameLogoLanguage { get; set;}

		[Ordinal(6)] [RED("m_fxSetText")] 		public CHandle<CScriptedFlashFunction> M_fxSetText { get; set;}

		public CR4StartScreenMenuBase(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CR4StartScreenMenuBase(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}