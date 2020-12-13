using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEstate.Formats.AC.Entity
{
    public class AnimationFrame : IGetExplorerInfo
    {
        public readonly Frame[] Frames;
        public readonly AnimationHook[] Hooks;

        public AnimationFrame(BinaryReader r, uint numParts)
        {
            Frames = r.ReadTArray(x => new Frame(r), (int)numParts);
            Hooks = r.ReadL32Array(AnimationHook.Factory);
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"Frames", items: Frames.Select(x => new ExplorerInfoNode($"{x}"))),
                Hooks.Length > 0 ? new ExplorerInfoNode($"Hooks", items: Hooks.Select(x => new ExplorerInfoNode($"{x}"))) : null,
            };
            return nodes;
        }
    }
}
