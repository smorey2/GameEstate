using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class WmkMapMenu : CObject
	{
		[Ordinal(1)] [RED("QUEST_PIN_TYPE_LOW")] 		public CName QUEST_PIN_TYPE_LOW { get; set;}

		[Ordinal(2)] [RED("QUEST_PIN_TYPE")] 		public CName QUEST_PIN_TYPE { get; set;}

		[Ordinal(3)] [RED("QUEST_PIN_TYPE_HIGH")] 		public CName QUEST_PIN_TYPE_HIGH { get; set;}

		[Ordinal(4)] [RED("QUEST_PIN_TYPE_DEADLY")] 		public CName QUEST_PIN_TYPE_DEADLY { get; set;}

		[Ordinal(5)] [RED("QUEST_PIN_ROTATION")] 		public CInt32 QUEST_PIN_ROTATION { get; set;}

		[Ordinal(6)] [RED("FILTER_LABEL")] 		public CString FILTER_LABEL { get; set;}

		[Ordinal(7)] [RED("CACHE_QUEST_PIN_POSITIONS")] 		public CBool CACHE_QUEST_PIN_POSITIONS { get; set;}

		[Ordinal(8)] [RED("LOG_ENABLED")] 		public CBool LOG_ENABLED { get; set;}

		[Ordinal(9)] [RED("m_thePlayer")] 		public CHandle<CPlayer> M_thePlayer { get; set;}

		[Ordinal(10)] [RED("m_commonMapManager")] 		public CHandle<CCommonMapManager> M_commonMapManager { get; set;}

		[Ordinal(11)] [RED("m_journalManager")] 		public CHandle<CWitcherJournalManager> M_journalManager { get; set;}

		[Ordinal(12)] [RED("m_isNewGamePlus")] 		public CBool M_isNewGamePlus { get; set;}

		[Ordinal(13)] [RED("m_cachedQuestMapPins", 2,0)] 		public CArray<WmkQuestMapPin> M_cachedQuestMapPins { get; set;}

		[Ordinal(14)] [RED("m_quickUpdateEntityPins")] 		public CBool M_quickUpdateEntityPins { get; set;}

		[Ordinal(15)] [RED("m_mapMenu")] 		public CHandle<CR4MapMenu> M_mapMenu { get; set;}

		[Ordinal(16)] [RED("m_shownArea")] 		public CEnum<EAreaName> M_shownArea { get; set;}

		[Ordinal(17)] [RED("m_questMapPinInstances", 2,0)] 		public CArray<SCommonMapPinInstance> M_questMapPinInstances { get; set;}

		[Ordinal(18)] [RED("m_questMapPins", 2,0)] 		public CArray<WmkQuestMapPin> M_questMapPins { get; set;}

		[Ordinal(19)] [RED("m_currentTrackedQuest")] 		public CHandle<CJournalQuest> M_currentTrackedQuest { get; set;}

		[Ordinal(20)] [RED("m_currentHighlightedObjective")] 		public CHandle<CJournalQuestObjective> M_currentHighlightedObjective { get; set;}

		public WmkMapMenu(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new WmkMapMenu(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}