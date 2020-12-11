using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class BTCondIsInTheWay : IBehTreeTask
	{
		[Ordinal(1)] [RED("origin")] 		public CEnum<ETargetName> Origin { get; set;}

		[Ordinal(2)] [RED("obstacle")] 		public CEnum<ETargetName> Obstacle { get; set;}

		[Ordinal(3)] [RED("destination")] 		public CEnum<ETargetName> Destination { get; set;}

		[Ordinal(4)] [RED("returnIfInvalid")] 		public CBool ReturnIfInvalid { get; set;}

		[Ordinal(5)] [RED("requiredDistanceFromLine")] 		public CFloat RequiredDistanceFromLine { get; set;}

		public BTCondIsInTheWay(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new BTCondIsInTheWay(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}