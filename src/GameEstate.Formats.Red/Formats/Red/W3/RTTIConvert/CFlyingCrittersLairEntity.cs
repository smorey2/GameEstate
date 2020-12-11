using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CFlyingCrittersLairEntity : CSwarmLairEntity
	{
		[Ordinal(1)] [RED("scriptInput")] 		public CPtr<CFlyingSwarmScriptInput> ScriptInput { get; set;}

		[Ordinal(2)] [RED("cellMapResourceFile")] 		public CSoft<CSwarmCellMap> CellMapResourceFile { get; set;}

		[Ordinal(3)] [RED("cellMapCellSize")] 		public CFloat CellMapCellSize { get; set;}

		public CFlyingCrittersLairEntity(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CFlyingCrittersLairEntity(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}