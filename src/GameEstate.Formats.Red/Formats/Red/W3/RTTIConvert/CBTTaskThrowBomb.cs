using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskThrowBomb : CBTTaskAttack
	{
		[Ordinal(1)] [RED("thrownEntity")] 		public CHandle<W3Petard> ThrownEntity { get; set;}

		[Ordinal(2)] [RED("inventory")] 		public CHandle<CInventoryComponent> Inventory { get; set;}

		[Ordinal(3)] [RED("bombs", 2,0)] 		public CArray<SItemUniqueId> Bombs { get; set;}

		[Ordinal(4)] [RED("cachedTargetPos")] 		public Vector CachedTargetPos { get; set;}

		[Ordinal(5)] [RED("dontUseDiwmeritium")] 		public CBool DontUseDiwmeritium { get; set;}

		[Ordinal(6)] [RED("activationChance")] 		public CFloat ActivationChance { get; set;}

		public CBTTaskThrowBomb(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskThrowBomb(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}