using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SCutsceneActorDef : CVariable
	{
		[Ordinal(1)] [RED("name")] 		public CString Name { get; set;}

		[Ordinal(2)] [RED("tag")] 		public TagList Tag { get; set;}

		[Ordinal(3)] [RED("voiceTag")] 		public CName VoiceTag { get; set;}

		[Ordinal(4)] [RED("template")] 		public CSoft<CEntityTemplate> Template { get; set;}

		[Ordinal(5)] [RED("appearance")] 		public CName Appearance { get; set;}

		[Ordinal(6)] [RED("type")] 		public CEnum<ECutsceneActorType> Type { get; set;}

		[Ordinal(7)] [RED("finalPosition")] 		public TagList FinalPosition { get; set;}

		[Ordinal(8)] [RED("killMe")] 		public CBool KillMe { get; set;}

		[Ordinal(9)] [RED("useMimic")] 		public CBool UseMimic { get; set;}

		[Ordinal(10)] [RED("animationAtFinalPosition")] 		public CName AnimationAtFinalPosition { get; set;}

		public SCutsceneActorDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SCutsceneActorDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}