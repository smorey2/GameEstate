using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class TaskManageVisibility : IBehTreeTask
	{
		[Ordinal(1)] [RED("visible")] 		public CBool Visible { get; set;}

		[Ordinal(2)] [RED("changeMeshVisibility")] 		public CBool ChangeMeshVisibility { get; set;}

		[Ordinal(3)] [RED("changeGameplayVisibility")] 		public CBool ChangeGameplayVisibility { get; set;}

		[Ordinal(4)] [RED("onActivate")] 		public CBool OnActivate { get; set;}

		[Ordinal(5)] [RED("onDeactivate")] 		public CBool OnDeactivate { get; set;}

		[Ordinal(6)] [RED("onAnimEvent")] 		public CBool OnAnimEvent { get; set;}

		[Ordinal(7)] [RED("onAnimEventName")] 		public CName OnAnimEventName { get; set;}

		public TaskManageVisibility(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new TaskManageVisibility(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}