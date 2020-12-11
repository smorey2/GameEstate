using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SReactionSceneEvent : CVariable
	{
		[Ordinal(1)] [RED("reactionScene")] 		public CName ReactionScene { get; set;}

		[Ordinal(2)] [RED("requiredSceneInputs", 2,0)] 		public CArray<CString> RequiredSceneInputs { get; set;}

		[Ordinal(3)] [RED("inputsAsymetric")] 		public CBool InputsAsymetric { get; set;}

		[Ordinal(4)] [RED("workOnlyBroadcast")] 		public CBool WorkOnlyBroadcast { get; set;}

		public SReactionSceneEvent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SReactionSceneEvent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}