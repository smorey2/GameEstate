using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CR4MainGammaMenu : CR4MenuBase
	{
		[Ordinal(1)] [RED("mInGameConfigWrapper")] 		public CHandle<CInGameConfigWrapper> MInGameConfigWrapper { get; set;}

		[Ordinal(2)] [RED("m_fxSetCurrentUsername")] 		public CHandle<CScriptedFlashFunction> M_fxSetCurrentUsername { get; set;}

		public CR4MainGammaMenu(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CR4MainGammaMenu(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}