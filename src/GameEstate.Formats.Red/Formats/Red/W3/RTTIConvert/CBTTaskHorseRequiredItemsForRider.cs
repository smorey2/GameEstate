using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskHorseRequiredItemsForRider : IBehTreeTask
	{
		[Ordinal(1)] [RED("processLeftItem")] 		public CBool ProcessLeftItem { get; set;}

		[Ordinal(2)] [RED("processRightItem")] 		public CBool ProcessRightItem { get; set;}

		[Ordinal(3)] [RED("LeftItemType")] 		public CName LeftItemType { get; set;}

		[Ordinal(4)] [RED("RightItemType")] 		public CName RightItemType { get; set;}

		public CBTTaskHorseRequiredItemsForRider(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskHorseRequiredItemsForRider(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}