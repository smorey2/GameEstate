using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskRaiseAnimationEventDef : IBehTreeTaskDefinition
	{
		[Ordinal(1)] [RED("eventName")] 		public CName EventName { get; set;}

		[Ordinal(2)] [RED("forceEvent")] 		public CBool ForceEvent { get; set;}

		[Ordinal(3)] [RED("onActivate")] 		public CBool OnActivate { get; set;}

		[Ordinal(4)] [RED("onDeactivate")] 		public CBool OnDeactivate { get; set;}

		[Ordinal(5)] [RED("raiseEventAfter")] 		public CFloat RaiseEventAfter { get; set;}

		public CBTTaskRaiseAnimationEventDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskRaiseAnimationEventDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}