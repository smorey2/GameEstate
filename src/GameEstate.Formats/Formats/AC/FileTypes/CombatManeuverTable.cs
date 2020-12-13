using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Entity;
using GameEstate.Formats.AC.Props;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameEstate.Formats.AC.FileTypes
{
    /// <summary>
    /// These are client_portal.dat files starting with 0x30. 
    /// </summary>
    [PakFileType(PakFileType.CombatTable)]
    public class CombatManeuverTable : AbstractFileType, IGetExplorerInfo
    {
        public readonly CombatManeuver[] CMT;
        public readonly Dictionary<MotionStance, AttackHeights> Stances;

        public CombatManeuverTable(BinaryReader r)
        {
            Id = r.ReadUInt32(); // This should always equal the fileId
            CMT = r.ReadL32Array(x => new CombatManeuver(x));
            Stances = new Dictionary<MotionStance, AttackHeights>();
            foreach (var maneuver in CMT)
            {
                if (!Stances.TryGetValue(maneuver.Style, out var attackHeights))
                    Stances.Add(maneuver.Style, new AttackHeights());
                if (!attackHeights.Table.TryGetValue(maneuver.AttackHeight, out var attackTypes))
                    attackHeights.Table.Add(maneuver.AttackHeight, new AttackTypes());
                if (!attackTypes.Table.TryGetValue(maneuver.AttackType, out var motionCommands))
                    attackTypes.Table.Add(maneuver.AttackType, new List<MotionCommand>());
                motionCommands.Add(maneuver.Motion);
            }
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(CombatManeuverTable)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                    //new ExplorerInfoNode($"Type: {Type}"),
                })
            };
            return nodes;
        }

        public override string ToString()
        {
            var b = new StringBuilder();
            foreach (var stance in Stances)
            {
                b.AppendLine($"- {stance.Key}");
                foreach (var attackHeight in stance.Value.Table)
                {
                    b.AppendLine($"  - {attackHeight.Key}");
                    foreach (var attackType in attackHeight.Value.Table)
                    {
                        b.AppendLine($"    - {attackType.Key}");
                        foreach (var motion in attackType.Value)
                            b.AppendLine($"      - {motion}");
                    }
                }
            }
            return b.ToString();
        }


        public class AttackHeights
        {
            public readonly Dictionary<AttackHeight, AttackTypes> Table = new Dictionary<AttackHeight, AttackTypes>();
        }

        public class AttackTypes
        {
            // technically there is another MinSkillLevels here in the data,
            // but every MinSkillLevel in the client dats are always 0
            public readonly Dictionary<AttackType, List<MotionCommand>> Table = new Dictionary<AttackType, List<MotionCommand>>();
        }

        public static readonly List<MotionCommand> Invalid = new List<MotionCommand>() { MotionCommand.Invalid };

        public List<MotionCommand> GetMotion(MotionStance stance, AttackHeight attackHeight, AttackType attackType, MotionCommand prevMotion)
        {
            if (!Stances.TryGetValue(stance, out var attackHeights))
                return Invalid;
            if (!attackHeights.Table.TryGetValue(attackHeight, out var attackTypes))
                return Invalid;
            if (!attackTypes.Table.TryGetValue(attackType, out var maneuvers))
                return Invalid;

            //if (maneuvers.Count == 1)
            //return maneuvers[0];

            /*Console.WriteLine($"CombatManeuverTable({Id:X8}).GetMotion({stance}, {attackHeight}, {attackType}) - found {maneuvers.Count} maneuvers");
            foreach (var maneuver in maneuvers)
                Console.WriteLine(maneuver);*/

            // CombatManeuverTable(30000000).GetMotion(SwordCombat, Medium, Slash) - found 2 maneuvers
            // SlashMed
            // BackhandMed

            // rng, or alternate?
            /*for (var i = 0; i < maneuvers.Count; i++)
            {
                var maneuver = maneuvers[i];

                if (maneuver == prevMotion)
                {
                    if (i < maneuvers.Count - 1)
                        return maneuvers[i + 1];
                    else
                        return maneuvers[0];
                }
            }
            return maneuvers[0];*/

            // if the CMT contains > 1 entries for this lookup, return both
            // the code determines which motion to use based on the power bar
            return maneuvers;
        }


    }
}
