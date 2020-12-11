using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehTreeTaskAgony : IBehTreeTask
	{
		[Ordinal(1)] [RED("agonyTime")] 		public CInt32 AgonyTime { get; set;}

		[Ordinal(2)] [RED("syncInstance")] 		public CHandle<CAnimationManualSlotSyncInstance> SyncInstance { get; set;}

		[Ordinal(3)] [RED("disableAgony")] 		public CBool DisableAgony { get; set;}

		[Ordinal(4)] [RED("chance")] 		public CInt32 Chance { get; set;}

		[Ordinal(5)] [RED("forceAgony")] 		public CBool ForceAgony { get; set;}

		public CBehTreeTaskAgony(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehTreeTaskAgony(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}