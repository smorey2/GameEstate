using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3SummonerComponent : CScriptedComponent
	{
		[Ordinal(1)] [RED("forgetDeadEntities")] 		public CBool ForgetDeadEntities { get; set;}

		[Ordinal(2)] [RED("m_SummonedEntities", 2,0)] 		public CArray<CHandle<CEntity>> M_SummonedEntities { get; set;}

		public W3SummonerComponent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3SummonerComponent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}