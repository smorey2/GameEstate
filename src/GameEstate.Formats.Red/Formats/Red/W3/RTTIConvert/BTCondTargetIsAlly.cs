using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class BTCondTargetIsAlly : IBehTreeTask
	{
		[Ordinal(1)] [RED("useNamedTarget")] 		public CName UseNamedTarget { get; set;}

		[Ordinal(2)] [RED("useCombatTarget")] 		public CBool UseCombatTarget { get; set;}

		[Ordinal(3)] [RED("saveTargetOnGameplayEvents", 2,0)] 		public CArray<CName> SaveTargetOnGameplayEvents { get; set;}

		[Ordinal(4)] [RED("m_Target")] 		public CHandle<CActor> M_Target { get; set;}

		public BTCondTargetIsAlly(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new BTCondTargetIsAlly(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}