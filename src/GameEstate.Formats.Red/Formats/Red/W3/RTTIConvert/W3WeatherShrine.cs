using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3WeatherShrine : CGameplayEntity
	{
		[Ordinal(1)] [RED("weatherBlendTime")] 		public CFloat WeatherBlendTime { get; set;}

		[Ordinal(2)] [RED("cooldown")] 		public CFloat Cooldown { get; set;}

		[Ordinal(3)] [RED("prayerForSunAcceptedFX")] 		public CName PrayerForSunAcceptedFX { get; set;}

		[Ordinal(4)] [RED("prayerForStormAcceptedFX")] 		public CName PrayerForStormAcceptedFX { get; set;}

		[Ordinal(5)] [RED("price")] 		public CInt32 Price { get; set;}

		public W3WeatherShrine(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3WeatherShrine(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}