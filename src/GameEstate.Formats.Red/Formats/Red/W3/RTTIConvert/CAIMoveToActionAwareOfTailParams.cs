using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAIMoveToActionAwareOfTailParams : CAIMoveToParams
	{
		[Ordinal(1)] [RED("tailTag")] 		public CName TailTag { get; set;}

		[Ordinal(2)] [RED("startMovementDistance")] 		public CFloat StartMovementDistance { get; set;}

		[Ordinal(3)] [RED("stopDistance")] 		public CFloat StopDistance { get; set;}

		public CAIMoveToActionAwareOfTailParams(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAIMoveToActionAwareOfTailParams(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}