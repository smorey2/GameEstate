using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class IInstantEffectExecutor : IGameplayEffectExecutor
	{
		[Ordinal(1)] [RED("customIconPath")] 		public CString CustomIconPath { get; set;}

		[Ordinal(2)] [RED("customNameLocalisationKey")] 		public CString CustomNameLocalisationKey { get; set;}

		[Ordinal(3)] [RED("customDescriptionLocalisationKey")] 		public CString CustomDescriptionLocalisationKey { get; set;}

		[Ordinal(4)] [RED("executorName")] 		public CName ExecutorName { get; set;}

		public IInstantEffectExecutor(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new IInstantEffectExecutor(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}