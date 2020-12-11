using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskChangeStance : IBehTreeTask
	{
		[Ordinal(1)] [RED("newStance")] 		public CEnum<ENpcStance> NewStance { get; set;}

		[Ordinal(2)] [RED("setPrevStanceOnDeactivation")] 		public CBool SetPrevStanceOnDeactivation { get; set;}

		[Ordinal(3)] [RED("oldStance")] 		public CEnum<ENpcStance> OldStance { get; set;}

		[Ordinal(4)] [RED("onDeactivate")] 		public CBool OnDeactivate { get; set;}

		[Ordinal(5)] [RED("changeToFlyOnlyIfAboveGround")] 		public CBool ChangeToFlyOnlyIfAboveGround { get; set;}

		public CBTTaskChangeStance(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskChangeStance(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}