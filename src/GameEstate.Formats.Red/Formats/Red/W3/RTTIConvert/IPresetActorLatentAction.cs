using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class IPresetActorLatentAction : IActorLatentAction
	{
		[Ordinal(1)] [RED("res")] 		public CHandle<CBehTree> Res { get; set;}

		[Ordinal(2)] [RED("def")] 		public CPtr<CBehTreeNodeTemplateDefinition> Def { get; set;}

		[Ordinal(3)] [RED("resName")] 		public CString ResName { get; set;}

		public IPresetActorLatentAction(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new IPresetActorLatentAction(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}