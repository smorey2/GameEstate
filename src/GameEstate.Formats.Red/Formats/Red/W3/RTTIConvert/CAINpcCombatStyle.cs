using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAINpcCombatStyle : CAISubTree
	{
		[Ordinal(1)] [RED("params")] 		public CHandle<CAINpcCombatStyleParams> Params { get; set;}

		[Ordinal(2)] [RED("highPriority")] 		public CBool HighPriority { get; set;}

		public CAINpcCombatStyle(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAINpcCombatStyle(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}