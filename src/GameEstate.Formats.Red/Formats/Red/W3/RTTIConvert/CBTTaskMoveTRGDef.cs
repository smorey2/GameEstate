using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskMoveTRGDef : IBehTreeTaskDefinition
	{
		[Ordinal(1)] [RED("fleeDistance")] 		public CFloat FleeDistance { get; set;}

		[Ordinal(2)] [RED("activationDistance")] 		public CFloat ActivationDistance { get; set;}

		[Ordinal(3)] [RED("ignoreEntityWithTag")] 		public CName IgnoreEntityWithTag { get; set;}

		[Ordinal(4)] [RED("flee")] 		public CBool Flee { get; set;}

		public CBTTaskMoveTRGDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskMoveTRGDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}