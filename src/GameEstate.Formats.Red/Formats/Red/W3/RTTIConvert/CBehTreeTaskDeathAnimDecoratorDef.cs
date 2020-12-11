using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehTreeTaskDeathAnimDecoratorDef : IBehTreeTaskDefinition
	{
		[Ordinal(1)] [RED("completeTimer")] 		public CFloat CompleteTimer { get; set;}

		[Ordinal(2)] [RED("disableCollisionOnAnim")] 		public CBehTreeValBool DisableCollisionOnAnim { get; set;}

		[Ordinal(3)] [RED("disableCollisionOnAnimDelay")] 		public CBehTreeValFloat DisableCollisionOnAnimDelay { get; set;}

		[Ordinal(4)] [RED("stopFXOnActivate")] 		public CBehTreeValCName StopFXOnActivate { get; set;}

		[Ordinal(5)] [RED("stopFXOnDeactivate")] 		public CBehTreeValCName StopFXOnDeactivate { get; set;}

		[Ordinal(6)] [RED("playFXOnActivate")] 		public CBehTreeValCName PlayFXOnActivate { get; set;}

		[Ordinal(7)] [RED("playFXOnDeactivate")] 		public CBehTreeValCName PlayFXOnDeactivate { get; set;}

		[Ordinal(8)] [RED("playSFXOnActivate")] 		public CBehTreeValCName PlaySFXOnActivate { get; set;}

		public CBehTreeTaskDeathAnimDecoratorDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehTreeTaskDeathAnimDecoratorDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}