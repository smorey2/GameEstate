using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class BTTaskGetFoodNearbyDef : IBehTreeTaskDefinition
	{
		[Ordinal(1)] [RED("corpse")] 		public CBehTreeValBool Corpse { get; set;}

		[Ordinal(2)] [RED("meat")] 		public CBehTreeValBool Meat { get; set;}

		[Ordinal(3)] [RED("vegetable")] 		public CBehTreeValBool Vegetable { get; set;}

		[Ordinal(4)] [RED("water")] 		public CBehTreeValBool Water { get; set;}

		[Ordinal(5)] [RED("monster")] 		public CBehTreeValBool Monster { get; set;}

		[Ordinal(6)] [RED("completeIfTargetChange")] 		public CBool CompleteIfTargetChange { get; set;}

		public BTTaskGetFoodNearbyDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new BTTaskGetFoodNearbyDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}