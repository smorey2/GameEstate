using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphRandomAnimTimeNode : CBehaviorGraphBaseNode
	{
		[Ordinal(1)] [RED("animSpeedMin")] 		public CFloat AnimSpeedMin { get; set;}

		[Ordinal(2)] [RED("animSpeedMax")] 		public CFloat AnimSpeedMax { get; set;}

		[Ordinal(3)] [RED("animStartTimeOffset")] 		public CFloat AnimStartTimeOffset { get; set;}

		[Ordinal(4)] [RED("animStartTimePrecent")] 		public CFloat AnimStartTimePrecent { get; set;}

		public CBehaviorGraphRandomAnimTimeNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphRandomAnimTimeNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}