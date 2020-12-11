using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskChanceDef : IBehTreeTaskDefinition
	{
		[Ordinal(1)] [RED("chance")] 		public CBehTreeValInt Chance { get; set;}

		[Ordinal(2)] [RED("frequency")] 		public CFloat Frequency { get; set;}

		[Ordinal(3)] [RED("scaleWithNumberOfOpponents")] 		public CBool ScaleWithNumberOfOpponents { get; set;}

		[Ordinal(4)] [RED("chancePerOpponent")] 		public CInt32 ChancePerOpponent { get; set;}

		public CBTTaskChanceDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskChanceDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}