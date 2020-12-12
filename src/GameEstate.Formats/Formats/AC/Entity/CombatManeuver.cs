using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class CombatManeuver
    {
        public readonly MotionStance Style;
        public readonly AttackHeight AttackHeight;
        public readonly AttackType AttackType;
        public readonly uint MinSkillLevel;
        public readonly MotionCommand Motion;

        public CombatManeuver(BinaryReader r)
        {
            Style = (MotionStance)r.ReadUInt32();
            AttackHeight = (AttackHeight)r.ReadUInt32();
            AttackType = (AttackType)r.ReadUInt32();
            MinSkillLevel = r.ReadUInt32();
            Motion = (MotionCommand)r.ReadUInt32();
        }
    }
}
