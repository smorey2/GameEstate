using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CFXTrackItemParameterFloat : CFXTrackItemCurveBase
	{
		[Ordinal(1)] [RED("parameterName")] 		public CName ParameterName { get; set;}

		[Ordinal(2)] [RED("restoreAtEnd")] 		public CBool RestoreAtEnd { get; set;}

		[Ordinal(3)] [RED("allComponents")] 		public CBool AllComponents { get; set;}

		public CFXTrackItemParameterFloat(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CFXTrackItemParameterFloat(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}