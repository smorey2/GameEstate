using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphValueAccNode : CBehaviorGraphValueBaseNode
	{
		[Ordinal(1)] [RED("resetOnActivation")] 		public CBool ResetOnActivation { get; set;}

		[Ordinal(2)] [RED("initValue")] 		public CFloat InitValue { get; set;}

		[Ordinal(3)] [RED("wrapValue")] 		public CBool WrapValue { get; set;}

		[Ordinal(4)] [RED("wrapValueThrMax")] 		public CFloat WrapValueThrMax { get; set;}

		[Ordinal(5)] [RED("wrapValueThrMin")] 		public CFloat WrapValueThrMin { get; set;}

		public CBehaviorGraphValueAccNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphValueAccNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}