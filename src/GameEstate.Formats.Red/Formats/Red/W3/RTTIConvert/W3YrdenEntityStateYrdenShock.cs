using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3YrdenEntityStateYrdenShock : W3SignEntityStateActive
	{
		[Ordinal(1)] [RED("usedShockAreaName")] 		public CName UsedShockAreaName { get; set;}

		[Ordinal(2)] [RED("traceFrom")] 		public Vector TraceFrom { get; set;}

		[Ordinal(3)] [RED("traceTo")] 		public Vector TraceTo { get; set;}

		public W3YrdenEntityStateYrdenShock(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3YrdenEntityStateYrdenShock(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}