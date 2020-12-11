using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3BlockGameplayActionsTrigger : CGameplayEntity
	{
		[Ordinal(1)] [RED("blockedActions", 2,0)] 		public CArray<CEnum<EInputActionBlock>> BlockedActions { get; set;}

		[Ordinal(2)] [RED("sourceName")] 		public CName SourceName { get; set;}

		[Ordinal(3)] [RED("sheatheWeaponIfDrawn")] 		public CBool SheatheWeaponIfDrawn { get; set;}

		public W3BlockGameplayActionsTrigger(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3BlockGameplayActionsTrigger(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}