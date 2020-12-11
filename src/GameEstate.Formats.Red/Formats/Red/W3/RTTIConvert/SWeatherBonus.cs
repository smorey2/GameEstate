using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SWeatherBonus : CVariable
	{
		[Ordinal(1)] [RED("dayPart")] 		public CEnum<EDayPart> DayPart { get; set;}

		[Ordinal(2)] [RED("weather")] 		public CEnum<EWeatherEffect> Weather { get; set;}

		[Ordinal(3)] [RED("moonState")] 		public CEnum<EMoonState> MoonState { get; set;}

		[Ordinal(4)] [RED("ability")] 		public CName Ability { get; set;}

		public SWeatherBonus(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SWeatherBonus(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}