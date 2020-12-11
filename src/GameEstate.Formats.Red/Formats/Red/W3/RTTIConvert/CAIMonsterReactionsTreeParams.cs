using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAIMonsterReactionsTreeParams : CAIReactionsParameters
	{
		[Ordinal(1)] [RED("reactions", 2,0)] 		public CArray<CHandle<CAIMonsterActionSubtree>> Reactions { get; set;}

		[Ordinal(2)] [RED("canFly")] 		public CBool CanFly { get; set;}

		public CAIMonsterReactionsTreeParams(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAIMonsterReactionsTreeParams(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}