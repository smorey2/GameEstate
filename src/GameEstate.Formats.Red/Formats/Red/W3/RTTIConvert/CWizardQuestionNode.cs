using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CWizardQuestionNode : CWizardBaseNode
	{
		[Ordinal(1)] [RED("uniqueName")] 		public CName UniqueName { get; set;}

		[Ordinal(2)] [RED("layoutTemplate")] 		public CString LayoutTemplate { get; set;}

		[Ordinal(3)] [RED("text")] 		public CString Text { get; set;}

		[Ordinal(4)] [RED("optional")] 		public CBool Optional { get; set;}

		[Ordinal(5)] [RED("endNode")] 		public CBool EndNode { get; set;}

		public CWizardQuestionNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CWizardQuestionNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}