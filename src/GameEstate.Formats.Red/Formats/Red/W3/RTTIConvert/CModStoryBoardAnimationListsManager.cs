using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CModStoryBoardAnimationListsManager : CObject
	{
		[Ordinal(1)] [RED("compatibleAnimationCount")] 		public CInt32 CompatibleAnimationCount { get; set;}

		[Ordinal(2)] [RED("dataLoaded")] 		public CBool DataLoaded { get; set;}

		[Ordinal(3)] [RED("animMeta")] 		public CHandle<CStoryBoardAnimationMetaInfo> AnimMeta { get; set;}

		public CModStoryBoardAnimationListsManager(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CModStoryBoardAnimationListsManager(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}