using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskHorseSendInfoDef : IBehTreeHorseTaskDefinition
	{
		[Ordinal(1)] [RED("onIsAvailable")] 		public CBool OnIsAvailable { get; set;}

		[Ordinal(2)] [RED("onActivate")] 		public CBool OnActivate { get; set;}

		[Ordinal(3)] [RED("onDectivate")] 		public CBool OnDectivate { get; set;}

		[Ordinal(4)] [RED("infoType")] 		public CEnum<EActionInfoType> InfoType { get; set;}

		[Ordinal(5)] [RED("notifyPlayerInsteadOfCombatTarget")] 		public CBool NotifyPlayerInsteadOfCombatTarget { get; set;}

		public CBTTaskHorseSendInfoDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskHorseSendInfoDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}