using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CPlanarShapeComponent : CBoundedComponent
	{
		[Ordinal(1)] [RED("localPoints", 2,0)] 		public CArray<Vector> LocalPoints { get; set;}

		[Ordinal(2)] [RED("worldPoints", 2,0)] 		public CArray<Vector> WorldPoints { get; set;}

		public CPlanarShapeComponent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CPlanarShapeComponent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}