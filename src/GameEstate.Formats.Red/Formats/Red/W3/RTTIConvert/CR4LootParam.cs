using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CR4LootParam : CGameplayEntityParam
	{
		[Ordinal(1)] [RED("containers", 2,0)] 		public CArray<CR4LootContainerParam> Containers { get; set;}

		[Ordinal(2)] [RED("usedContainersMin")] 		public CUInt32 UsedContainersMin { get; set;}

		[Ordinal(3)] [RED("usedContainersMax")] 		public CUInt32 UsedContainersMax { get; set;}

		[Ordinal(4)] [RED("alwaysPresent")] 		public CBool AlwaysPresent { get; set;}

		public CR4LootParam(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CR4LootParam(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}