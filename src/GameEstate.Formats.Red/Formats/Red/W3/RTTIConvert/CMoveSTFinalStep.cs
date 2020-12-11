using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CMoveSTFinalStep : CMoveSTMove
	{
		[Ordinal(1)] [RED("ignoreGoalToleranceForFinalLocation")] 		public CBool IgnoreGoalToleranceForFinalLocation { get; set;}

		[Ordinal(2)] [RED("finalStepPositionVar")] 		public CName FinalStepPositionVar { get; set;}

		[Ordinal(3)] [RED("finalStepDistanceVar")] 		public CName FinalStepDistanceVar { get; set;}

		[Ordinal(4)] [RED("finalStepActiveVar")] 		public CName FinalStepActiveVar { get; set;}

		[Ordinal(5)] [RED("finalStepEvent")] 		public CName FinalStepEvent { get; set;}

		[Ordinal(6)] [RED("finalStepActivationNotification")] 		public CName FinalStepActivationNotification { get; set;}

		[Ordinal(7)] [RED("finalStepDeactivationNotification")] 		public CName FinalStepDeactivationNotification { get; set;}

		[Ordinal(8)] [RED("finalStepDeactivationNotificationTimeOut")] 		public CFloat FinalStepDeactivationNotificationTimeOut { get; set;}

		[Ordinal(9)] [RED("finalStepDistanceLimit")] 		public CFloat FinalStepDistanceLimit { get; set;}

		public CMoveSTFinalStep(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CMoveSTFinalStep(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}