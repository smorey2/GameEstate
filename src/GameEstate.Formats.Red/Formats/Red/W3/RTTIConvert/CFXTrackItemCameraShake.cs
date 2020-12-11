using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CFXTrackItemCameraShake : CFXTrackItem
	{
		[Ordinal(1)] [RED("effectFullForceRadius")] 		public CFloat EffectFullForceRadius { get; set;}

		[Ordinal(2)] [RED("effectMaxRadius")] 		public CFloat EffectMaxRadius { get; set;}

		[Ordinal(3)] [RED("shakeType")] 		public CName ShakeType { get; set;}

		public CFXTrackItemCameraShake(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CFXTrackItemCameraShake(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}