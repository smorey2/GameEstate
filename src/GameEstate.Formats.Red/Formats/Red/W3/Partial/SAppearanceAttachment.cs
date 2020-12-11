using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public partial class SAppearanceAttachment : CVariable
	{
		[Ordinal(1)] [RED("parentClass")] 		public CName ParentClass { get; set;}

		[Ordinal(2)] [RED("parentName")] 		public CName ParentName { get; set;}

		[Ordinal(3)] [RED("childClass")] 		public CName ChildClass { get; set;}

		[Ordinal(4)] [RED("childName")] 		public CName ChildName { get; set;}

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SAppearanceAttachment(cr2w, parent, name);

	}
}