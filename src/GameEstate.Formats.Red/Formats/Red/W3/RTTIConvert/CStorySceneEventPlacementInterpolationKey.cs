using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CStorySceneEventPlacementInterpolationKey : CVariable
	{
		[Ordinal(1)] [RED("bezierHandles", 6)] 		public CArrayFixedSize<Bezier2dHandle> BezierHandles { get; set;}

		[Ordinal(2)] [RED("interpolationTypes", 6)] 		public CArrayFixedSize<CUInt32> InterpolationTypes { get; set;}

		[Ordinal(3)] [RED("volatile")] 		public CBool Volatile { get; set;}

		public CStorySceneEventPlacementInterpolationKey(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CStorySceneEventPlacementInterpolationKey(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}