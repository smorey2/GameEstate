using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3FloeEntity : W3DestroyableTerrain
	{
		[Ordinal(1)] [RED("m_currentFxID")] 		public CInt32 M_currentFxID { get; set;}

		[Ordinal(2)] [RED("entryTime")] 		public CFloat EntryTime { get; set;}

		[Ordinal(3)] [RED("timerInterval")] 		public CFloat TimerInterval { get; set;}

		[Ordinal(4)] [RED("rot")] 		public EulerAngles Rot { get; set;}

		public W3FloeEntity(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3FloeEntity(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}