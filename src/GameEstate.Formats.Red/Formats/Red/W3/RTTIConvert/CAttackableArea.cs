using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAttackableArea : CGameplayEntityParam
	{
		[Ordinal(1)] [RED("offset")] 		public Vector Offset { get; set;}

		[Ordinal(2)] [RED("radius")] 		public CFloat Radius { get; set;}

		[Ordinal(3)] [RED("height")] 		public CFloat Height { get; set;}

		public CAttackableArea(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAttackableArea(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}