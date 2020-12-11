using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CR4HudModuleBossFocus : CR4HudModuleBase
	{
		[Ordinal(1)] [RED("m_bossEntity")] 		public CHandle<CActor> M_bossEntity { get; set;}

		[Ordinal(2)] [RED("m_bossName")] 		public CString M_bossName { get; set;}

		[Ordinal(3)] [RED("m_fxSetBossName")] 		public CHandle<CScriptedFlashFunction> M_fxSetBossName { get; set;}

		[Ordinal(4)] [RED("m_fxSetBossHealth")] 		public CHandle<CScriptedFlashFunction> M_fxSetBossHealth { get; set;}

		[Ordinal(5)] [RED("m_fxSetEssenceDamage")] 		public CHandle<CScriptedFlashFunction> M_fxSetEssenceDamage { get; set;}

		[Ordinal(6)] [RED("m_lastHealthPercentage")] 		public CInt32 M_lastHealthPercentage { get; set;}

		[Ordinal(7)] [RED("m_delay")] 		public CFloat M_delay { get; set;}

		public CR4HudModuleBossFocus(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CR4HudModuleBossFocus(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}