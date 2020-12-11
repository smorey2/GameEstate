using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SSbDescActor : CVariable
	{
		[Ordinal(1)] [RED("uId")] 		public CString UId { get; set;}

		[Ordinal(2)] [RED("repoActorId")] 		public CString RepoActorId { get; set;}

		[Ordinal(3)] [RED("template")] 		public CString Template { get; set;}

		[Ordinal(4)] [RED("appearance")] 		public CString Appearance { get; set;}

		[Ordinal(5)] [RED("isPlayer")] 		public CBool IsPlayer { get; set;}

		public SSbDescActor(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SSbDescActor(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}