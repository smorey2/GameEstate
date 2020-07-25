using GameEstate.Graphics;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace GameEstate.Formats.Valve.Blocks
{
    public class DATAMesh : IMeshInfo
    {
        public DATA Data { get; }

        public IVBIB VBIB { get; }

        public Vector3 MinBounds { get; private set; }
        public Vector3 MaxBounds { get; private set; }

        public DATAMesh(BinaryPak resource)
        {
            Data = resource.DATA;
            VBIB = resource.VBIB;
            GetBounds();
        }

        public DATAMesh(DATA data, IVBIB vbib)
        {
            Data = data;
            VBIB = vbib;
            GetBounds();
        }

        public IDictionary<string, object> GetData()
        {
            switch (Data)
            {
                case DATABinaryKV3 kv3: return kv3.Data;
                case DATABinaryNTRO ntro: return ntro.Data;
                default: throw new InvalidOperationException($"Unknown model data type {Data.GetType().Name}");
            }
        }

        void GetBounds()
        {
            var sceneObjects = GetData().GetArray("m_sceneObjects");
            if (sceneObjects.Length == 0)
            {
                MinBounds = MaxBounds = new Vector3(0, 0, 0);
                return;
            }
            var minBounds = sceneObjects[0].GetSub("m_vMinBounds").ToVector3();
            var maxBounds = sceneObjects[0].GetSub("m_vMaxBounds").ToVector3();
            for (var i = 1; i < sceneObjects.Length; ++i)
            {
                var localMin = sceneObjects[i].GetSub("m_vMinBounds").ToVector3();
                var localMax = sceneObjects[i].GetSub("m_vMaxBounds").ToVector3();
                minBounds.X = System.Math.Min(minBounds.X, localMin.X);
                minBounds.Y = System.Math.Min(minBounds.Y, localMin.Y);
                minBounds.Z = System.Math.Min(minBounds.Z, localMin.Z);
                maxBounds.X = System.Math.Max(maxBounds.X, localMax.X);
                maxBounds.Y = System.Math.Max(maxBounds.Y, localMax.Y);
                maxBounds.Z = System.Math.Max(maxBounds.Z, localMax.Z);
            }
            MinBounds = minBounds;
            MaxBounds = maxBounds;
        }
    }
}
