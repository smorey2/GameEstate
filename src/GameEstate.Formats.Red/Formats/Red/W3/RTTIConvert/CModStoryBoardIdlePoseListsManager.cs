using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CModStoryBoardIdlePoseListsManager : CObject
	{
		[Ordinal(1)] [RED("compatiblePosesCount")] 		public CInt32 CompatiblePosesCount { get; set;}

		[Ordinal(2)] [RED("dataLoaded")] 		public CBool DataLoaded { get; set;}

		[Ordinal(3)] [RED("idlePoseMeta")] 		public CHandle<CStoryBoardIdlePoseMetaInfo> IdlePoseMeta { get; set;}

		public CModStoryBoardIdlePoseListsManager(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CModStoryBoardIdlePoseListsManager(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}