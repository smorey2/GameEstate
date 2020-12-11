using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskToadBackProjectilesDef : CBTTaskProjectileAttackWithPrepareDef
	{
		[Ordinal(1)] [RED("minDistFromTarget")] 		public CFloat MinDistFromTarget { get; set;}

		[Ordinal(2)] [RED("maxDistFromTarget")] 		public CFloat MaxDistFromTarget { get; set;}

		[Ordinal(3)] [RED("range")] 		public CFloat Range { get; set;}

		[Ordinal(4)] [RED("animEvent", 2,0)] 		public CArray<CName> AnimEvent { get; set;}

		[Ordinal(5)] [RED("boneNames", 2,0)] 		public CArray<CName> BoneNames { get; set;}

		public CBTTaskToadBackProjectilesDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskToadBackProjectilesDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}