using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskMoveTRG : IBehTreeTask
	{
		[Ordinal(1)] [RED("activationDistance")] 		public CFloat ActivationDistance { get; set;}

		[Ordinal(2)] [RED("fleeDistance")] 		public CFloat FleeDistance { get; set;}

		[Ordinal(3)] [RED("ignoreEntityWithTag")] 		public CName IgnoreEntityWithTag { get; set;}

		[Ordinal(4)] [RED("dangerNode")] 		public CHandle<CNode> DangerNode { get; set;}

		[Ordinal(5)] [RED("flee")] 		public CBool Flee { get; set;}

		public CBTTaskMoveTRG(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskMoveTRG(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}