using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CR4HudModuleRadialMenu : CR4HudModuleBase
	{
		[Ordinal(1)] [RED("m_flashValueStorage")] 		public CHandle<CScriptedFlashValueStorage> M_flashValueStorage { get; set;}

		[Ordinal(2)] [RED("m_fxBlockRadialMenuSFF")] 		public CHandle<CScriptedFlashFunction> M_fxBlockRadialMenuSFF { get; set;}

		[Ordinal(3)] [RED("m_fxShowRadialMenuSFF")] 		public CHandle<CScriptedFlashFunction> M_fxShowRadialMenuSFF { get; set;}

		[Ordinal(4)] [RED("m_fxUpdateItemIconSFF")] 		public CHandle<CScriptedFlashFunction> M_fxUpdateItemIconSFF { get; set;}

		[Ordinal(5)] [RED("m_fxUpdateFieldEquippedStateSFF")] 		public CHandle<CScriptedFlashFunction> M_fxUpdateFieldEquippedStateSFF { get; set;}

		[Ordinal(6)] [RED("m_fxSetDesaturatedSFF")] 		public CHandle<CScriptedFlashFunction> M_fxSetDesaturatedSFF { get; set;}

		[Ordinal(7)] [RED("m_fxSetCiriRadialSFF")] 		public CHandle<CScriptedFlashFunction> M_fxSetCiriRadialSFF { get; set;}

		[Ordinal(8)] [RED("m_fxSetCiriItemSFF")] 		public CHandle<CScriptedFlashFunction> M_fxSetCiriItemSFF { get; set;}

		[Ordinal(9)] [RED("m_fxSetMeditationButtonEnabledSFF")] 		public CHandle<CScriptedFlashFunction> M_fxSetMeditationButtonEnabledSFF { get; set;}

		[Ordinal(10)] [RED("m_fxSetSelectedItem")] 		public CHandle<CScriptedFlashFunction> M_fxSetSelectedItem { get; set;}

		[Ordinal(11)] [RED("m_fxSetArabicAligmentMode")] 		public CHandle<CScriptedFlashFunction> M_fxSetArabicAligmentMode { get; set;}

		[Ordinal(12)] [RED("m_fxUpdateInputMode")] 		public CHandle<CScriptedFlashFunction> M_fxUpdateInputMode { get; set;}

		[Ordinal(13)] [RED("m_shown")] 		public CBool M_shown { get; set;}

		[Ordinal(14)] [RED("m_IsPlayerCiri")] 		public CBool M_IsPlayerCiri { get; set;}

		[Ordinal(15)] [RED("m_isDesaturated")] 		public CBool M_isDesaturated { get; set;}

		[Ordinal(16)] [RED("m_HasBlink")] 		public CBool M_HasBlink { get; set;}

		[Ordinal(17)] [RED("m_HasCharge")] 		public CBool M_HasCharge { get; set;}

		[Ordinal(18)] [RED("m_allowAutoRotationReturnValue")] 		public CBool M_allowAutoRotationReturnValue { get; set;}

		[Ordinal(19)] [RED("m_swappedAcceptCancel")] 		public CBool M_swappedAcceptCancel { get; set;}

		[Ordinal(20)] [RED("m_tutorialsHidden")] 		public CBool M_tutorialsHidden { get; set;}

		[Ordinal(21)] [RED("_currentSelection")] 		public CString _currentSelection { get; set;}

		public CR4HudModuleRadialMenu(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CR4HudModuleRadialMenu(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}