using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTask3StageIdle : IBehTreeTask
	{
		[Ordinal(1)] [RED("minTime")] 		public CFloat MinTime { get; set;}

		[Ordinal(2)] [RED("maxTime")] 		public CFloat MaxTime { get; set;}

		[Ordinal(3)] [RED("loopTime")] 		public CFloat LoopTime { get; set;}

		public CBTTask3StageIdle(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTask3StageIdle(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}