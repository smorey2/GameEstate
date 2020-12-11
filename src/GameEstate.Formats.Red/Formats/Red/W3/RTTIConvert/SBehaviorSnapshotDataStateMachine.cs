using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SBehaviorSnapshotDataStateMachine : CVariable
	{
		[Ordinal(1)] [RED("stateMachineId")] 		public CUInt32 StateMachineId { get; set;}

		[Ordinal(2)] [RED("currentStateId")] 		public CUInt32 CurrentStateId { get; set;}

		[Ordinal(3)] [RED("currentStateTime")] 		public CFloat CurrentStateTime { get; set;}

		public SBehaviorSnapshotDataStateMachine(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SBehaviorSnapshotDataStateMachine(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}