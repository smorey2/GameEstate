using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAIAttachToCurve : IAIActionTree
	{
		[Ordinal(1)] [RED("animationName")] 		public CName AnimationName { get; set;}

		[Ordinal(2)] [RED("curveTag")] 		public CName CurveTag { get; set;}

		[Ordinal(3)] [RED("curveDummyName")] 		public CString CurveDummyName { get; set;}

		[Ordinal(4)] [RED("blendInTime")] 		public CFloat BlendInTime { get; set;}

		[Ordinal(5)] [RED("slotAnimation")] 		public CName SlotAnimation { get; set;}

		public CAIAttachToCurve(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAIAttachToCurve(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}