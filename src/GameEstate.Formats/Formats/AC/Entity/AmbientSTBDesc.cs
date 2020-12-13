using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEstate.Formats.AC.Entity
{
    public class AmbientSTBDesc : IGetExplorerInfo
    {
        public readonly uint STBId;
        public readonly AmbientSoundDesc[] AmbientSounds;

        public AmbientSTBDesc(BinaryReader r)
        {
            STBId = r.ReadUInt32();
            AmbientSounds = r.ReadL32Array(x => new AmbientSoundDesc(x));
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"Ambient Sound Table ID: {STBId:X8}"),
                new ExplorerInfoNode($"Ambient Sounds", items: AmbientSounds.Select((x, i) => new ExplorerInfoNode($"{i}", items: (x as IGetExplorerInfo).GetInfoNodes()))),
            };
            return nodes;
        }
    }
}
