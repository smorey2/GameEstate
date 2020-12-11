using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CExtAnimCutsceneSetClippingPlanesEvent : CExtAnimEvent
	{
		[Ordinal(1)] [RED("nearPlaneDistance")] 		public CEnum<ENearPlaneDistance> NearPlaneDistance { get; set;}

		[Ordinal(2)] [RED("farPlaneDistance")] 		public CEnum<EFarPlaneDistance> FarPlaneDistance { get; set;}

		[Ordinal(3)] [RED("customPlaneDistance")] 		public SCustomClippingPlanes CustomPlaneDistance { get; set;}

		public CExtAnimCutsceneSetClippingPlanesEvent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CExtAnimCutsceneSetClippingPlanesEvent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}