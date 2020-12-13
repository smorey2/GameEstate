using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Props;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace GameEstate.Formats.AC.Entity
{
    public class MotionData : IGetExplorerInfo
    {
        public readonly byte Bitfield;
        public readonly MotionDataFlags Flags;
        public readonly AnimData[] Anims;
        public readonly Vector3 Velocity;
        public readonly Vector3 Omega;

        public MotionData(BinaryReader r)
        {
            var numAnims = r.ReadByte();
            Bitfield = r.ReadByte();
            Flags = (MotionDataFlags)r.ReadByte();
            r.AlignBoundary();
            Anims = r.ReadTArray(x => new AnimData(x), numAnims);
            if ((Flags & MotionDataFlags.HasVelocity) != 0)
                Velocity = r.ReadVector3();
            if ((Flags & MotionDataFlags.HasOmega) != 0)
                Omega = r.ReadVector3();
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                Bitfield != 0 ? new ExplorerInfoNode($"Bitfield: {Bitfield:X8}") : null,
                Anims.Length == 0 ? null : Anims.Length == 1
                    ? new ExplorerInfoNode("Animation", items: (Anims[0] as IGetExplorerInfo).GetInfoNodes())
                    : new ExplorerInfoNode("Animation", items: Anims.Select((x, i) => new ExplorerInfoNode($"{i}", items: (x as IGetExplorerInfo).GetInfoNodes()))),
                Flags.HasFlag(MotionDataFlags.HasVelocity) ? new ExplorerInfoNode($"Velocity: {Velocity}") : null,
                Flags.HasFlag(MotionDataFlags.HasOmega) ? new ExplorerInfoNode($"Omega: {Omega}") : null,
            };
            return nodes;
        }
    }
}
