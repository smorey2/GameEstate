using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskWaitFor : IBehTreeTask
	{
		[Ordinal(1)] [RED("waitForTag")] 		public CName WaitForTag { get; set;}

		[Ordinal(2)] [RED("timeout")] 		public CFloat Timeout { get; set;}

		[Ordinal(3)] [RED("testDistance")] 		public CFloat TestDistance { get; set;}

		[Ordinal(4)] [RED("timeoutCounter")] 		public CFloat TimeoutCounter { get; set;}

		public CBTTaskWaitFor(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskWaitFor(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}