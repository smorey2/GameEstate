using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphPoseSlotNode : CBehaviorGraphBaseNode
	{
		[Ordinal(1)] [RED("slotName")] 		public CName SlotName { get; set;}

		[Ordinal(2)] [RED("firstBone")] 		public CString FirstBone { get; set;}

		[Ordinal(3)] [RED("worldSpace")] 		public CBool WorldSpace { get; set;}

		[Ordinal(4)] [RED("interpolation")] 		public CEnum<EInterpolationType> Interpolation { get; set;}

		[Ordinal(5)] [RED("blendFloatTracks")] 		public CBool BlendFloatTracks { get; set;}

		[Ordinal(6)] [RED("ignoreZeroFloatTracks")] 		public CBool IgnoreZeroFloatTracks { get; set;}

		public CBehaviorGraphPoseSlotNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphPoseSlotNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}