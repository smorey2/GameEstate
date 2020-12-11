using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta(EREDMetaInfo.REDStruct)]
	public partial class CSectorData : ISerializable
	{
		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CSectorData(cr2w, parent, name);

	}
}