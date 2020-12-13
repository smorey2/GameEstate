using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Props;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class CombatManeuver : IGetExplorerInfo
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

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"Stance: {Style}"),
                new ExplorerInfoNode($"Attack Height: {AttackHeight}"),
                new ExplorerInfoNode($"Attack Type: {AttackType}"),
                MinSkillLevel != 0 ? new ExplorerInfoNode($"Min Skill: {MinSkillLevel}") : null,
                new ExplorerInfoNode($"Motion: {Motion}"),
            };
            return nodes;
        }
    }
}
