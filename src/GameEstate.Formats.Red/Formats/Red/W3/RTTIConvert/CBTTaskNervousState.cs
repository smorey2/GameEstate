using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskNervousState : IBehTreeTask
	{
		[Ordinal(1)] [RED("dangerRadius")] 		public CFloat DangerRadius { get; set;}

		[Ordinal(2)] [RED("rearingChance")] 		public CFloat RearingChance { get; set;}

		[Ordinal(3)] [RED("kickChance")] 		public CFloat KickChance { get; set;}

		[Ordinal(4)] [RED("callFromQuestOnly")] 		public CBool CallFromQuestOnly { get; set;}

		[Ordinal(5)] [RED("force")] 		public CBool Force { get; set;}

		[Ordinal(6)] [RED("called")] 		public CBool Called { get; set;}

		[Ordinal(7)] [RED("dangerNode")] 		public CHandle<CNode> DangerNode { get; set;}

		public CBTTaskNervousState(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskNervousState(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}