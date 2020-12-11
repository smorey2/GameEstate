using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class BTTaskDrainStat : IBehTreeTask
	{
		[Ordinal(1)] [RED("stat")] 		public CEnum<EBaseCharacterStats> Stat { get; set;}

		[Ordinal(2)] [RED("val")] 		public CFloat Val { get; set;}

		[Ordinal(3)] [RED("onActivate")] 		public CBool OnActivate { get; set;}

		public BTTaskDrainStat(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new BTTaskDrainStat(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}