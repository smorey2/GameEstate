using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehTreeTaskCriticalState : IBehTreeTask
	{
		[Ordinal(1)] [RED("activate")] 		public CBool Activate { get; set;}

		[Ordinal(2)] [RED("activateTimeStamp")] 		public CFloat ActivateTimeStamp { get; set;}

		[Ordinal(3)] [RED("forceActivate")] 		public CBool ForceActivate { get; set;}

		[Ordinal(4)] [RED("currentCS")] 		public CEnum<ECriticalStateType> CurrentCS { get; set;}

		public CBehTreeTaskCriticalState(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehTreeTaskCriticalState(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}