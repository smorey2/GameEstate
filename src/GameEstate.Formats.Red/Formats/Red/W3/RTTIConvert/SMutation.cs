using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SMutation : CVariable
	{
		[Ordinal(1)] [RED("type")] 		public CEnum<EPlayerMutationType> Type { get; set;}

		[Ordinal(2)] [RED("colors", 2,0)] 		public CArray<CEnum<ESkillColor>> Colors { get; set;}

		[Ordinal(3)] [RED("progress")] 		public SMutationProgress Progress { get; set;}

		[Ordinal(4)] [RED("requiredMutations", 2,0)] 		public CArray<CEnum<EPlayerMutationType>> RequiredMutations { get; set;}

		[Ordinal(5)] [RED("localizationNameKey")] 		public CName LocalizationNameKey { get; set;}

		[Ordinal(6)] [RED("localizationDescriptionKey")] 		public CName LocalizationDescriptionKey { get; set;}

		[Ordinal(7)] [RED("iconPath")] 		public CName IconPath { get; set;}

		[Ordinal(8)] [RED("soundbank")] 		public CString Soundbank { get; set;}

		public SMutation(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SMutation(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}