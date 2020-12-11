using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3NPCBackground : CGameplayEntity
	{
		[Ordinal(1)] [RED("work")] 		public CEnum<EBackgroundNPCWork_Single> Work { get; set;}

		[Ordinal(2)] [RED("parentPairedBackgroundNPCEntity")] 		public CHandle<W3NPCBackgroundPair> ParentPairedBackgroundNPCEntity { get; set;}

		[Ordinal(3)] [RED("isWorkingSingle")] 		public CBool IsWorkingSingle { get; set;}

		public W3NPCBackground(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3NPCBackground(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}