using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CreditsSection : CVariable
	{
		[Ordinal(1)] [RED("sectionName")] 		public CString SectionName { get; set;}

		[Ordinal(2)] [RED("positionNames", 2,0)] 		public CArray<CString> PositionNames { get; set;}

		[Ordinal(3)] [RED("crewNames", 2,0)] 		public CArray<CString> CrewNames { get; set;}

		[Ordinal(4)] [RED("displayTime")] 		public CFloat DisplayTime { get; set;}

		[Ordinal(5)] [RED("positionX")] 		public CInt32 PositionX { get; set;}

		[Ordinal(6)] [RED("positionY")] 		public CInt32 PositionY { get; set;}

		[Ordinal(7)] [RED("delay")] 		public CFloat Delay { get; set;}

		public CreditsSection(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CreditsSection(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}