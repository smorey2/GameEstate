using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3SE_ManageClue : W3SwitchEvent
	{
		[Ordinal(1)] [RED("clueHandle", 2,0)] 		public CArray<EntityHandle> ClueHandle { get; set;}

		[Ordinal(2)] [RED("clueTag")] 		public CName ClueTag { get; set;}

		[Ordinal(3)] [RED("operations", 2,0)] 		public CArray<CEnum<EClueOperation>> Operations { get; set;}

		[Ordinal(4)] [RED("myTags", 2,0)] 		public CArray<CName> MyTags { get; set;}

		public W3SE_ManageClue(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3SE_ManageClue(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}