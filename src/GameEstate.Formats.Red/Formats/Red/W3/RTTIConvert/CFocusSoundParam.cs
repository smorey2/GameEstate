using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CFocusSoundParam : CGameplayEntityParam
	{
		[Ordinal(1)] [RED("eventStart")] 		public CName EventStart { get; set;}

		[Ordinal(2)] [RED("eventStop")] 		public CName EventStop { get; set;}

		[Ordinal(3)] [RED("hearingAngle")] 		public CFloat HearingAngle { get; set;}

		[Ordinal(4)] [RED("visualEffectBoneName")] 		public CName VisualEffectBoneName { get; set;}

		public CFocusSoundParam(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CFocusSoundParam(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}