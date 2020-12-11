using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CClimbType : CVariable
	{
		[Ordinal(1)] [RED("requiredState")] 		public CEnum<EClimbRequirementType> RequiredState { get; set;}

		[Ordinal(2)] [RED("requiredVault")] 		public CEnum<EClimbRequirementVault> RequiredVault { get; set;}

		[Ordinal(3)] [RED("requiredPlatform")] 		public CEnum<EClimbRequirementPlatform> RequiredPlatform { get; set;}

		[Ordinal(4)] [RED("type")] 		public CEnum<EClimbHeightType> Type { get; set;}

		[Ordinal(5)] [RED("heightUseDefaults")] 		public CBool HeightUseDefaults { get; set;}

		[Ordinal(6)] [RED("heightMax")] 		public CFloat HeightMax { get; set;}

		[Ordinal(7)] [RED("heightMin")] 		public CFloat HeightMin { get; set;}

		[Ordinal(8)] [RED("heightExact")] 		public CFloat HeightExact { get; set;}

		[Ordinal(9)] [RED("forwardDistExact")] 		public CFloat ForwardDistExact { get; set;}

		[Ordinal(10)] [RED("playCameraAnimation")] 		public CBool PlayCameraAnimation { get; set;}

		[Ordinal(11)] [RED("cameraAnimation")] 		public CName CameraAnimation { get; set;}

		public CClimbType(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CClimbType(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}