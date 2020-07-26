// https://github.com/dolkensp/unp4k/releases
namespace GameEstate.Formats.Binary
{
    /// <summary>
    /// PakBinaryP4k
    /// </summary>
    /// <seealso cref="GameEstate.Formats.Binary.PakBinaryZip" />
    public class PakBinaryP4k : PakBinaryZip
    {
        public new static readonly PakBinary Instance = new PakBinaryP4k();
        PakBinaryP4k() : base(P4kKey) { }

        static readonly byte[] P4kKey = new byte[] { 0x5E, 0x7A, 0x20, 0x02, 0x30, 0x2E, 0xEB, 0x1A, 0x3B, 0xB6, 0x17, 0xC3, 0x0F, 0xDE, 0x1E, 0x47 };
    }
}