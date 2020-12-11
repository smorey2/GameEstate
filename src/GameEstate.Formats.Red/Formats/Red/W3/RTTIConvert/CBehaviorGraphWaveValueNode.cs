using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphWaveValueNode : CBehaviorGraphValueBaseNode
	{
		[Ordinal(1)] [RED("type")] 		public CEnum<EBehaviorWaveValueType> Type { get; set;}

		[Ordinal(2)] [RED("freq")] 		public CFloat Freq { get; set;}

		[Ordinal(3)] [RED("amp")] 		public CFloat Amp { get; set;}

		public CBehaviorGraphWaveValueNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphWaveValueNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}