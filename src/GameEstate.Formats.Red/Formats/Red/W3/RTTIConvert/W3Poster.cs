using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3Poster : CGameplayEntity
	{
		[Ordinal(1)] [RED("descriptionGenerated")] 		public CBool DescriptionGenerated { get; set;}

		[Ordinal(2)] [RED("description")] 		public CString Description { get; set;}

		[Ordinal(3)] [RED("camera")] 		public CHandle<CEntityTemplate> Camera { get; set;}

		[Ordinal(4)] [RED("factOnRead")] 		public CString FactOnRead { get; set;}

		[Ordinal(5)] [RED("factOnInteraction")] 		public CString FactOnInteraction { get; set;}

		[Ordinal(6)] [RED("blendInTime")] 		public CFloat BlendInTime { get; set;}

		[Ordinal(7)] [RED("blendOutTime")] 		public CFloat BlendOutTime { get; set;}

		[Ordinal(8)] [RED("fadeStartDuration")] 		public CFloat FadeStartDuration { get; set;}

		[Ordinal(9)] [RED("fadeEndDuration")] 		public CFloat FadeEndDuration { get; set;}

		[Ordinal(10)] [RED("focusModeHighlight")] 		public CEnum<EFocusModeVisibility> FocusModeHighlight { get; set;}

		[Ordinal(11)] [RED("alignLeft")] 		public CBool AlignLeft { get; set;}

		[Ordinal(12)] [RED("restoreUsableItemAtEnd")] 		public CBool RestoreUsableItemAtEnd { get; set;}

		[Ordinal(13)] [RED("spawnedCamera")] 		public CHandle<CStaticCamera> SpawnedCamera { get; set;}

		public W3Poster(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3Poster(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}