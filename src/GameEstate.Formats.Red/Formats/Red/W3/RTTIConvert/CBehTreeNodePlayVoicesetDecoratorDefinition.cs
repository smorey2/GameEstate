using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehTreeNodePlayVoicesetDecoratorDefinition : IBehTreeNodeSpeechDecoratorDefinition
	{
		[Ordinal(1)] [RED("voiceSet")] 		public CBehTreeValString VoiceSet { get; set;}

		[Ordinal(2)] [RED("voicePriority")] 		public CBehTreeValInt VoicePriority { get; set;}

		[Ordinal(3)] [RED("minSpeechDelay")] 		public CFloat MinSpeechDelay { get; set;}

		[Ordinal(4)] [RED("maxSpeechDelay")] 		public CFloat MaxSpeechDelay { get; set;}

		[Ordinal(5)] [RED("waitUntilSpeechIsFinished")] 		public CBool WaitUntilSpeechIsFinished { get; set;}

		[Ordinal(6)] [RED("dontActivateWhileSpeaking")] 		public CBool DontActivateWhileSpeaking { get; set;}

		public CBehTreeNodePlayVoicesetDecoratorDefinition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehTreeNodePlayVoicesetDecoratorDefinition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}