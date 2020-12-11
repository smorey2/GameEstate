using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SStoryBoardActorStateData : CVariable
	{
		[Ordinal(1)] [RED("id")] 		public CString Id { get; set;}

		[Ordinal(2)] [RED("assetname")] 		public CString Assetname { get; set;}

		[Ordinal(3)] [RED("userSetName")] 		public CBool UserSetName { get; set;}

		[Ordinal(4)] [RED("templatePath")] 		public CString TemplatePath { get; set;}

		[Ordinal(5)] [RED("appearanceId")] 		public CInt32 AppearanceId { get; set;}

		[Ordinal(6)] [RED("defaultIdleAnim")] 		public CName DefaultIdleAnim { get; set;}

		public SStoryBoardActorStateData(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SStoryBoardActorStateData(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}