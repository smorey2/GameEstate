using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CQuestFactsDBConditionBase : IQuestCondition
	{
		[Ordinal(1)] [RED("factId")] 		public CString FactId { get; set;}

		[Ordinal(2)] [RED("queryFact")] 		public CEnum<EQueryFact> QueryFact { get; set;}

		[Ordinal(3)] [RED("value")] 		public CInt32 Value { get; set;}

		[Ordinal(4)] [RED("compareFunc")] 		public CEnum<ECompareFunc> CompareFunc { get; set;}

		public CQuestFactsDBConditionBase(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CQuestFactsDBConditionBase(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}