using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CExtAnimCutsceneWindEvent : CExtAnimCutsceneDurationEvent
	{
		[Ordinal(1)] [RED("enabled")] 		public CBool Enabled { get; set;}

		[Ordinal(2)] [RED("factor")] 		public CFloat Factor { get; set;}

		[Ordinal(3)] [RED("useWeightCurve")] 		public CBool UseWeightCurve { get; set;}

		[Ordinal(4)] [RED("weightCurve")] 		public SCurveData WeightCurve { get; set;}

		public CExtAnimCutsceneWindEvent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CExtAnimCutsceneWindEvent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}