using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class SkyObjectReplace : IGetExplorerInfo
    {
        public readonly uint ObjectIndex;
        public readonly uint GFXObjId;
        public readonly float Rotate;
        public readonly float Transparent;
        public readonly float Luminosity;
        public readonly float MaxBright;

        public SkyObjectReplace(BinaryReader r)
        {
            ObjectIndex = r.ReadUInt32();
            GFXObjId = r.ReadUInt32();
            Rotate = r.ReadSingle();
            Transparent = r.ReadSingle();
            Luminosity = r.ReadSingle();
            MaxBright = r.ReadSingle();
            r.AlignBoundary();
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"ObjIdx: {ObjectIndex}"),
                GFXObjId != 0 ? new ExplorerInfoNode($"GfxObjID: {GFXObjId:X8}") : null,
                Rotate != 0 ? new ExplorerInfoNode($"Rotate: {Rotate}") : null,
                Transparent != 0 ? new ExplorerInfoNode($"Transparent: {Transparent}") : null,
                Luminosity != 0 ? new ExplorerInfoNode($"Luminosity: {Luminosity}") : null,
                MaxBright != 0 ? new ExplorerInfoNode($"MaxBright: {MaxBright}") : null,
            };
            return nodes;
        }
    }
}
