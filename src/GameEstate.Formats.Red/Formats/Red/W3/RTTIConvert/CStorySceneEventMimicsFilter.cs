using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CStorySceneEventMimicsFilter : CStorySceneEventDuration
	{
		[Ordinal(1)] [RED("actor")] 		public CName Actor { get; set;}

		[Ordinal(2)] [RED("filterName")] 		public CName FilterName { get; set;}

		[Ordinal(3)] [RED("weight")] 		public CFloat Weight { get; set;}

		[Ordinal(4)] [RED("useWeightCurve")] 		public CBool UseWeightCurve { get; set;}

		[Ordinal(5)] [RED("weightCurve")] 		public SCurveData WeightCurve { get; set;}

		public CStorySceneEventMimicsFilter(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CStorySceneEventMimicsFilter(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}