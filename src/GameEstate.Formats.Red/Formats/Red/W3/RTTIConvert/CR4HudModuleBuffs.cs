using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CR4HudModuleBuffs : CR4HudModuleBase
	{
		[Ordinal(1)] [RED("_currentEffects", 2,0)] 		public CArray<CHandle<CBaseGameplayEffect>> _currentEffects { get; set;}

		[Ordinal(2)] [RED("_previousEffects", 2,0)] 		public CArray<CHandle<CBaseGameplayEffect>> _previousEffects { get; set;}

		[Ordinal(3)] [RED("_forceUpdate")] 		public CBool _forceUpdate { get; set;}

		[Ordinal(4)] [RED("m_fxSetPercentSFF")] 		public CHandle<CScriptedFlashFunction> M_fxSetPercentSFF { get; set;}

		[Ordinal(5)] [RED("m_fxShowBuffUpdateFx")] 		public CHandle<CScriptedFlashFunction> M_fxShowBuffUpdateFx { get; set;}

		[Ordinal(6)] [RED("m_fxsetViewMode")] 		public CHandle<CScriptedFlashFunction> M_fxsetViewMode { get; set;}

		[Ordinal(7)] [RED("m_flashValueStorage")] 		public CHandle<CScriptedFlashValueStorage> M_flashValueStorage { get; set;}

		[Ordinal(8)] [RED("iCurrentEffectsSize")] 		public CInt32 ICurrentEffectsSize { get; set;}

		[Ordinal(9)] [RED("bDisplayBuffs")] 		public CBool BDisplayBuffs { get; set;}

		[Ordinal(10)] [RED("m_runword5Applied")] 		public CBool M_runword5Applied { get; set;}

		public CR4HudModuleBuffs(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CR4HudModuleBuffs(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}