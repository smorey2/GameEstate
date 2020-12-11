using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SFurDetailLOD : CVariable
	{
		[Ordinal(1)] [RED("enableDetailLOD")] 		public CBool EnableDetailLOD { get; set;}

		[Ordinal(2)] [RED("detailLODStart")] 		public CFloat DetailLODStart { get; set;}

		[Ordinal(3)] [RED("detailLODEnd")] 		public CFloat DetailLODEnd { get; set;}

		[Ordinal(4)] [RED("detailLODWidth")] 		public CFloat DetailLODWidth { get; set;}

		[Ordinal(5)] [RED("detailLODDensity")] 		public CFloat DetailLODDensity { get; set;}

		public SFurDetailLOD(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SFurDetailLOD(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}