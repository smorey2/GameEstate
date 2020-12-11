using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CR4HudModuleDamagedItems : CR4HudModuleBase
	{
		[Ordinal(1)] [RED("m_fxSetItemDamaged")] 		public CHandle<CScriptedFlashFunction> M_fxSetItemDamaged { get; set;}

		[Ordinal(2)] [RED("damagedItems", 2,0)] 		public CArray<CBool> DamagedItems { get; set;}

		[Ordinal(3)] [RED("inv")] 		public CHandle<CInventoryComponent> Inv { get; set;}

		[Ordinal(4)] [RED("isDisplayed")] 		public CBool IsDisplayed { get; set;}

		public CR4HudModuleDamagedItems(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CR4HudModuleDamagedItems(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}