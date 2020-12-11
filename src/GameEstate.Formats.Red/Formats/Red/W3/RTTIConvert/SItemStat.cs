using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SItemStat : CVariable
	{
		[Ordinal(1)] [RED("statType")] 		public CName StatType { get; set;}

		[Ordinal(2)] [RED("statWeight")] 		public CFloat StatWeight { get; set;}

		[Ordinal(3)] [RED("statIsPercentage")] 		public CBool StatIsPercentage { get; set;}

		public SItemStat(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SItemStat(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}