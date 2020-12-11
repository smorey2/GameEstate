using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskLockViewToDettlaff : IBehTreeTask
	{
		[Ordinal(1)] [RED("actor")] 		public CHandle<CActor> Actor { get; set;}

		[Ordinal(2)] [RED("lock")] 		public CBool Lock { get; set;}

		public CBTTaskLockViewToDettlaff(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskLockViewToDettlaff(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}