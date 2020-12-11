using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public partial class CSkeletalAnimationSetEntry : ISerializable
	{
		[Ordinal(1)] [RED("animation")] 		public CPtr<CSkeletalAnimation> Animation { get; set;}

		[Ordinal(2)] [RED("compressedPoseBlend")] 		public CEnum<ECompressedPoseBlend> CompressedPoseBlend { get; set;}

		[Ordinal(3)] [RED("params", 2,0)] 		public CArray<CPtr<ISkeletalAnimationSetEntryParam>> Params { get; set;}

		[Ordinal(4)] [RED("eventsGroupsRanges", 2,0)] 		public CArray<SEventGroupsRanges> EventsGroupsRanges { get; set;}

		public CSkeletalAnimationSetEntry(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CSkeletalAnimationSetEntry(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}