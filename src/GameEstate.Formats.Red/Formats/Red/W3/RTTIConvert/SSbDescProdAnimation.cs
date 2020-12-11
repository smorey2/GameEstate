using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SSbDescProdAnimation : CVariable
	{
		[Ordinal(1)] [RED("prodAnimId")] 		public CString ProdAnimId { get; set;}

		[Ordinal(2)] [RED("prodActorId")] 		public CString ProdActorId { get; set;}

		[Ordinal(3)] [RED("repoAnimId")] 		public CString RepoAnimId { get; set;}

		public SSbDescProdAnimation(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SSbDescProdAnimation(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}