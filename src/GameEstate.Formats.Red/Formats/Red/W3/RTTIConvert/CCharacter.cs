using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CCharacter : CObject
	{
		[Ordinal(1)] [RED("name")] 		public CName Name { get; set;}

		[Ordinal(2)] [RED("guid")] 		public CGUID Guid { get; set;}

		[Ordinal(3)] [RED("parentCharacter")] 		public CPtr<CCharacter> ParentCharacter { get; set;}

		[Ordinal(4)] [RED("i_voiceTag")] 		public CName I_voiceTag { get; set;}

		[Ordinal(5)] [RED("tags")] 		public TagList Tags { get; set;}

		[Ordinal(6)] [RED("entityTemplate")] 		public CSoft<CEntityTemplate> EntityTemplate { get; set;}

		public CCharacter(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CCharacter(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}