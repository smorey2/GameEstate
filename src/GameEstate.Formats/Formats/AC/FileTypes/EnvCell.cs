using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using GameEstate.Formats.AC.Entity;
using GameEstate.Formats.AC.Props;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEstate.Formats.AC.FileTypes
{
    /// <summary>
    /// This reads an "indoor" cell from the client_cell.dat. This is mostly dungeons, but can also be a building interior.
    /// An EnvCell is designated by starting 0x0100 (whereas all landcells are in the 0x0001 - 0x003E range.
    /// <para />
    /// The fileId is the full int32/dword landblock value as reported by the @loc command (e.g. 0x12345678)
    /// </summary>
    /// <remarks>
    /// Very special thanks again to David Simpson for his early work on reading the cell.dat. Even bigger thanks for his documentation of it!
    /// </remarks>
    [PakFileType(PakFileType.EnvCell)]
    public class EnvCell : AbstractFileType, IGetExplorerInfo
    {
        public readonly EnvCellFlags Flags;
        public readonly uint[] Surfaces; // 0x08000000 surfaces (which contains degrade/quality info to reference the specific 0x06000000 graphics)
        public readonly uint EnvironmentId; // the 0x0D000000 model of the pre-fab dungeon block
        public readonly ushort CellStructure;
        public readonly Frame Position;
        public readonly CellPortal[] CellPortals;
        public readonly ushort[] VisibleCells;
        public readonly Stab[] StaticObjects;
        public readonly uint RestrictionObj;
        public bool SeenOutside => Flags.HasFlag(EnvCellFlags.SeenOutside);

        public EnvCell(BinaryReader r)
        {
            Id = r.ReadUInt32();
            Flags = (EnvCellFlags)r.ReadUInt32();
            r.BaseStream.Position += 4; // Skip ahead 4 bytes, because this is the CellId. Again. Twice.
            var numSurfaces = r.ReadByte();
            var numPortals = r.ReadByte(); // Note that "portal" in this context does not refer to the swirly pink/purple thing, its basically connecting cells
            var numStabs = r.ReadUInt16(); // I believe this is what cells can be seen from this one. So the engine knows what else it needs to load/draw.
            // Read what surfaces are used in this cell
            Surfaces = r.ReadTArray(x => 0x08000000u | r.ReadUInt16(), numSurfaces); // these are stored in the dat as short values, so we'll make them a full dword
            EnvironmentId = 0x0D000000u | r.ReadUInt16();
            CellStructure = r.ReadUInt16();
            Position = new Frame(r);
            CellPortals = r.ReadTArray(x => new CellPortal(x), numPortals);
            VisibleCells = r.ReadTArray<ushort>(sizeof(ushort), numStabs);
            if ((Flags & EnvCellFlags.HasStaticObjs) != 0)
                StaticObjects = r.ReadL32Array(x => new Stab(x));
            if ((Flags & EnvCellFlags.HasRestrictionObj) != 0)
                RestrictionObj = r.ReadUInt32();
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(EnvCell)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                    Flags != 0 ? new ExplorerInfoNode($"Flags: {Flags}") : null,
                    new ExplorerInfoNode("Surfaces", items: Surfaces.Select(x => new ExplorerInfoNode($"{x:X8}"))),
                    new ExplorerInfoNode($"Environment: {EnvironmentId:X8}"),
                    CellStructure != 0 ? new ExplorerInfoNode($"CellStructure: {CellStructure}") : null,
                    new ExplorerInfoNode($"Position: {Position}"),
                    CellPortals.Length > 0 ? new ExplorerInfoNode("CellPortals", items: CellPortals.Select((x, i) => new ExplorerInfoNode($"{i}", items: (x as IGetExplorerInfo).GetInfoNodes()))) : null,
                    StaticObjects.Length > 0 ? new ExplorerInfoNode("StaticObjects", items: StaticObjects.Select((x, i) => new ExplorerInfoNode($"{i}", items: (x as IGetExplorerInfo).GetInfoNodes()))) : null,
                    RestrictionObj != 0 ? new ExplorerInfoNode($"RestrictionObj: {RestrictionObj:X8}") : null,
                })
            };
            return nodes;
        }
    }
}
