using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.Valve.Blocks
{
    public class DATABinaryKV3OrNTRO : DATA
    {
        readonly string IntrospectionStructName;

        protected BinaryPak Resource { get; private set; }

        public IDictionary<string, object> Data { get; private set; }

        DATA BackingData;

        public DATABinaryKV3OrNTRO() { }
        public DATABinaryKV3OrNTRO(string introspectionStructName) => IntrospectionStructName = introspectionStructName;

        public override void Read(BinaryReader reader, BinaryPak resource)
        {
            Resource = resource;
            if (!resource.ContainsBlockType<NTRO>())
            {
                var kv3 = new DATABinaryKV3
                {
                    Offset = Offset,
                    Size = Size,
                };
                kv3.Read(reader, resource);
                Data = kv3.Data;
                BackingData = kv3;
            }
            else
            {
                var ntro = new DATABinaryNTRO
                {
                    StructName = IntrospectionStructName,
                    Offset = Offset,
                    Size = Size,
                };
                ntro.Read(reader, resource);
                Data = ntro.Data;
                BackingData = ntro;
            }
        }

        //public override string ToString()
        //{
        //    if (BackingData is DATABinaryKV3 kv3)
        //        return kv3.GetKV3File().ToString();
        //    return BackingData.ToString();
        //}
    }
}
