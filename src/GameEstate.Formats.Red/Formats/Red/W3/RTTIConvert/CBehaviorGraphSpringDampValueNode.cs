using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphSpringDampValueNode : CBehaviorGraphValueBaseNode
	{
		[Ordinal(1)] [RED("factor")] 		public CFloat Factor { get; set;}

		[Ordinal(2)] [RED("scale")] 		public CFloat Scale { get; set;}

		[Ordinal(3)] [RED("forceInputValueOnActivate")] 		public CBool ForceInputValueOnActivate { get; set;}

		public CBehaviorGraphSpringDampValueNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphSpringDampValueNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}