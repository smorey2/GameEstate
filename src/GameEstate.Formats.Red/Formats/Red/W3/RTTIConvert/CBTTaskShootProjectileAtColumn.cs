using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskShootProjectileAtColumn : IBehTreeTask
	{
		[Ordinal(1)] [RED("l_npc")] 		public CHandle<CNewNPC> L_npc { get; set;}

		[Ordinal(2)] [RED("l_projRot")] 		public EulerAngles L_projRot { get; set;}

		[Ordinal(3)] [RED("l_projPos")] 		public Vector L_projPos { get; set;}

		[Ordinal(4)] [RED("l_projectile")] 		public CHandle<W3AdvancedProjectile> L_projectile { get; set;}

		[Ordinal(5)] [RED("projEntity")] 		public CHandle<CEntityTemplate> ProjEntity { get; set;}

		[Ordinal(6)] [RED("l_columnArray", 2,0)] 		public CArray<CHandle<CEntity>> L_columnArray { get; set;}

		public CBTTaskShootProjectileAtColumn(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskShootProjectileAtColumn(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}