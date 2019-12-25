using GameEstate.Core;
using GameEstate.Core.DataFormat;

namespace GameEstate.Tes
{
    public class TesPakFile : CorePakFile
    {
        public TesPakFile(string filePath) : base(filePath, new PakFormat02(), new DatFormat02()) { }
    }
}