using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class IBehaviorConstraintObject : CObject
	{
		[Ordinal(1)] [RED("localPositionOffset")] 		public Vector LocalPositionOffset { get; set;}

		[Ordinal(2)] [RED("localRotationOffset")] 		public EulerAngles LocalRotationOffset { get; set;}

		public IBehaviorConstraintObject(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new IBehaviorConstraintObject(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}