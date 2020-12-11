using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CExtAnimCutsceneBokehDofBlendEvent : CExtAnimDurationEvent
	{
		[Ordinal(1)] [RED("bokehDofParamsStart")] 		public SBokehDofParams BokehDofParamsStart { get; set;}

		[Ordinal(2)] [RED("bokehDofParamsEnd")] 		public SBokehDofParams BokehDofParamsEnd { get; set;}

		public CExtAnimCutsceneBokehDofBlendEvent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CExtAnimCutsceneBokehDofBlendEvent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}