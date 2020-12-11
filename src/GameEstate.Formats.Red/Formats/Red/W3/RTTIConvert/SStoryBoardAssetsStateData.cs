using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SStoryBoardAssetsStateData : CVariable
	{
		[Ordinal(1)] [RED("lastuid")] 		public CInt32 Lastuid { get; set;}

		[Ordinal(2)] [RED("actorData", 2,0)] 		public CArray<SStoryBoardActorStateData> ActorData { get; set;}

		[Ordinal(3)] [RED("itemData", 2,0)] 		public CArray<SStoryBoardItemStateData> ItemData { get; set;}

		public SStoryBoardAssetsStateData(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SStoryBoardAssetsStateData(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}