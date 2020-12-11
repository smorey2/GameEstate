using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CR4HudModuleAnchors : CR4HudModuleBase
	{
		[Ordinal(1)] [RED("m_fxUpdateAnchorsPositions")] 		public CHandle<CScriptedFlashFunction> M_fxUpdateAnchorsPositions { get; set;}

		[Ordinal(2)] [RED("m_fxUpdateAnchorsAspectRatio")] 		public CHandle<CScriptedFlashFunction> M_fxUpdateAnchorsAspectRatio { get; set;}

		public CR4HudModuleAnchors(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CR4HudModuleAnchors(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}