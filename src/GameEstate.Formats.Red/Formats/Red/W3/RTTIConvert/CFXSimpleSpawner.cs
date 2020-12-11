using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CFXSimpleSpawner : IFXSpawner
	{
		[Ordinal(1)] [RED("slotNames", 2,0)] 		public CArray<CName> SlotNames { get; set;}

		[Ordinal(2)] [RED("boneNames", 2,0)] 		public CArray<CName> BoneNames { get; set;}

		[Ordinal(3)] [RED("relativePos")] 		public Vector RelativePos { get; set;}

		[Ordinal(4)] [RED("relativeRot")] 		public EulerAngles RelativeRot { get; set;}

		public CFXSimpleSpawner(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CFXSimpleSpawner(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}