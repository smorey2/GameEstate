using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3QuenEntityStateQuenChanneled : W3SignEntityStateChanneling
	{
		[Ordinal(1)] [RED("HEALING_FACTOR")] 		public CFloat HEALING_FACTOR { get; set;}

		[Ordinal(2)] [RED("HAXXOR_LeavingState")] 		public CBool HAXXOR_LeavingState { get; set;}

		public W3QuenEntityStateQuenChanneled(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3QuenEntityStateQuenChanneled(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}