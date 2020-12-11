using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CJobActionBase : CObject
	{
		[Ordinal(1)] [RED("categoryName")] 		public CString CategoryName { get; set;}

		[Ordinal(2)] [RED("animName")] 		public CName AnimName { get; set;}

		[Ordinal(3)] [RED("animBlendIn")] 		public CFloat AnimBlendIn { get; set;}

		[Ordinal(4)] [RED("animBlendOut")] 		public CFloat AnimBlendOut { get; set;}

		[Ordinal(5)] [RED("fireBlendedEvents")] 		public CBool FireBlendedEvents { get; set;}

		[Ordinal(6)] [RED("allowedLookAtLevel")] 		public CEnum<ELookAtLevel> AllowedLookAtLevel { get; set;}

		[Ordinal(7)] [RED("ignoreIfItemMounted")] 		public CName IgnoreIfItemMounted { get; set;}

		[Ordinal(8)] [RED("isSkippable")] 		public CBool IsSkippable { get; set;}

		public CJobActionBase(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CJobActionBase(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}