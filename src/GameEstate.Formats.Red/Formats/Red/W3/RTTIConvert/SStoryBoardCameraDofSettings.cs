using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SStoryBoardCameraDofSettings : CVariable
	{
		[Ordinal(1)] [RED("strength")] 		public CFloat Strength { get; set;}

		[Ordinal(2)] [RED("blurNear")] 		public CFloat BlurNear { get; set;}

		[Ordinal(3)] [RED("blurFar")] 		public CFloat BlurFar { get; set;}

		[Ordinal(4)] [RED("focusNear")] 		public CFloat FocusNear { get; set;}

		[Ordinal(5)] [RED("focusFar")] 		public CFloat FocusFar { get; set;}

		public SStoryBoardCameraDofSettings(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SStoryBoardCameraDofSettings(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}