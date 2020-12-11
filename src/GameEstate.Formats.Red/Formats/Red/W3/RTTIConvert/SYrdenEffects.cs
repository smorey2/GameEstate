using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SYrdenEffects : CVariable
	{
		[Ordinal(1)] [RED("castEffect")] 		public CName CastEffect { get; set;}

		[Ordinal(2)] [RED("placeEffect")] 		public CName PlaceEffect { get; set;}

		[Ordinal(3)] [RED("shootEffect")] 		public CName ShootEffect { get; set;}

		[Ordinal(4)] [RED("activateEffect")] 		public CName ActivateEffect { get; set;}

		public SYrdenEffects(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SYrdenEffects(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}