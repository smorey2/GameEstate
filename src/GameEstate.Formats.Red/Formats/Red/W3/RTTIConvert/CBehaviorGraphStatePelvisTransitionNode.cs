using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphStatePelvisTransitionNode : CBehaviorGraphStateTransitionBlendNode
	{
		[Ordinal(1)] [RED("pelvisBoneName")] 		public CString PelvisBoneName { get; set;}

		[Ordinal(2)] [RED("pelvisDirectionFwdLS")] 		public CEnum<EAxis> PelvisDirectionFwdLS { get; set;}

		[Ordinal(3)] [RED("usePelvisBlendMethod")] 		public CBool UsePelvisBlendMethod { get; set;}

		public CBehaviorGraphStatePelvisTransitionNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphStatePelvisTransitionNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}