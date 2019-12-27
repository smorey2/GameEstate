using GameEstate.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class PakFormat
    {
        public enum WriteState { Header, File }

        public readonly static PakFormat Default = new PakFormat00();

        public virtual Task ReadAsync(CorePakFile source, BinaryReader r) => throw new NotSupportedException();

        public virtual Task WriteAsync(CorePakFile source, BinaryWriter w, WriteState stage) => throw new NotSupportedException();
    }
}