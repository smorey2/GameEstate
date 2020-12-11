using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskStopEffectIfActive : IBehTreeTask
	{
		[Ordinal(1)] [RED("entity")] 		public CHandle<CEntity> Entity { get; set;}

		[Ordinal(2)] [RED("effectName")] 		public CName EffectName { get; set;}

		[Ordinal(3)] [RED("onActivate")] 		public CBool OnActivate { get; set;}

		[Ordinal(4)] [RED("onDeactivate")] 		public CBool OnDeactivate { get; set;}

		[Ordinal(5)] [RED("allEffects")] 		public CBool AllEffects { get; set;}

		[Ordinal(6)] [RED("findActorByTag")] 		public CBool FindActorByTag { get; set;}

		[Ordinal(7)] [RED("tagToFind")] 		public CName TagToFind { get; set;}

		public CBTTaskStopEffectIfActive(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskStopEffectIfActive(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}