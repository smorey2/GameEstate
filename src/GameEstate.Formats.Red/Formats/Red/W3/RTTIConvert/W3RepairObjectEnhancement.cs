using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3RepairObjectEnhancement : CBaseGameplayEffect
	{
		[Ordinal(1)] [RED("usesPerkBonus")] 		public CBool UsesPerkBonus { get; set;}

		[Ordinal(2)] [RED("durUpdates")] 		public CBool DurUpdates { get; set;}

		public W3RepairObjectEnhancement(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3RepairObjectEnhancement(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}