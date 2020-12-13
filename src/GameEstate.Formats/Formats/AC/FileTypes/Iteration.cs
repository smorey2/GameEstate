using GameEstate.Core;
using GameEstate.Explorer;
using GameEstate.Explorer.ViewModel;
using GameEstate.Formats._Packages;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameEstate.Formats.AC.FileTypes
{
    /// <summary>
    /// These are stored in the client_cell.dat, client_portal.dat, and client_local_English.dat files with the index 0xFFFF0001
    ///
    /// This is essentially the dat "versioning" system.
    /// This is used when first connecting to the server to compare the client dat files with the server dat files and any subsequent patching that may need to be done.
    /// 
    /// Special thanks to the GDLE team for pointing me the right direction on how/where to find this info in the dat files- OptimShi
    /// </summary>
    public class Iteration : AbstractFileType, IGetExplorerInfo
    {
        public const uint FILE_ID = 0xFFFF0001;

        public readonly int[] Ints;
        public readonly bool Sorted;

        public Iteration(BinaryReader r)
        {
            Ints = new[] { r.ReadInt32(), r.ReadInt32() };
            Sorted = r.ReadBoolean();
            r.AlignBoundary();
        }

        public override string ToString()
        {
            var b = new StringBuilder();
            for (var i = 0; i < Ints.Length; i++)
                b.Append($"{Ints[i]},");
            b.Append(Sorted ? "1" : "0");
            return b.ToString();
        }

        List<ExplorerInfoNode> IGetExplorerInfo.GetInfoNodes(ExplorerManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<ExplorerInfoNode> {
                new ExplorerInfoNode($"{nameof(Iteration)}: {Id:X8}", items: new List<ExplorerInfoNode> {
                    //new ExplorerInfoNode($"Type: {Type}"),
                })
            };
            return nodes;
        }
    }
}
