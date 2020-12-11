using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3SE_EnableTreasureHuntMappin : W3SwitchEvent
	{
		[Ordinal(1)] [RED("mappinEntityTag")] 		public CName MappinEntityTag { get; set;}

		[Ordinal(2)] [RED("enable")] 		public CBool Enable { get; set;}

		[Ordinal(3)] [RED("mappinEntity")] 		public CHandle<W3TreasureHuntMappinEntity> MappinEntity { get; set;}

		[Ordinal(4)] [RED("commonMapManager")] 		public CHandle<CCommonMapManager> CommonMapManager { get; set;}

		public W3SE_EnableTreasureHuntMappin(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3SE_EnableTreasureHuntMappin(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}