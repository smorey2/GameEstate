using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskManageSwimming : IBehTreeTask
	{
		[Ordinal(1)] [RED("onActivate")] 		public CBool OnActivate { get; set;}

		[Ordinal(2)] [RED("isSwimmingValue")] 		public CBool IsSwimmingValue { get; set;}

		[Ordinal(3)] [RED("m_isInWater")] 		public CBool M_isInWater { get; set;}

		[Ordinal(4)] [RED("m_isWaitingForWater")] 		public CBool M_isWaitingForWater { get; set;}

		public CBTTaskManageSwimming(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskManageSwimming(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}