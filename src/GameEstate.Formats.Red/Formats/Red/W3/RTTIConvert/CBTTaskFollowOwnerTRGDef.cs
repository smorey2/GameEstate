using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskFollowOwnerTRGDef : IBehTreeTaskDefinition
	{
		[Ordinal(1)] [RED("activationDistance")] 		public CFloat ActivationDistance { get; set;}

		[Ordinal(2)] [RED("minimumDistance")] 		public CFloat MinimumDistance { get; set;}

		[Ordinal(3)] [RED("ignoreEntityWithTag")] 		public CName IgnoreEntityWithTag { get; set;}

		public CBTTaskFollowOwnerTRGDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskFollowOwnerTRGDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}