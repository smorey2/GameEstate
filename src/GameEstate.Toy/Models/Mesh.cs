using GameEstate.Toy.Blocks;
using System;
using System.Numerics;

namespace GameEstate.Toy.Models
{
    public class Mesh
    {
        //public ResourceData Data { get; }

        public VBIB VBIB { get; }

        public Vector3 MinBounds { get; private set; }
        public Vector3 MaxBounds { get; private set; }

        //public Mesh(Resource resource)
        //{
        //    Data = resource.DataBlock;
        //    VBIB = resource.VBIB;
        //    GetBounds();
        //}

        //public Mesh(ResourceData data, VBIB vbib)
        //{
        //    Data = data;
        //    VBIB = vbib;
        //    GetBounds();
        //}

        //public IKeyValueCollection GetData()
        //{
        //    switch (Data)
        //    {
        //        case BinaryKV3 binaryKv: return binaryKv.Data;
        //        case NTRO ntro: return ntro.Output;
        //        default: throw new InvalidOperationException($"Unknown model data type {Data.GetType().Name}");
        //    }
        //}

        //void GetBounds()
        //{
        //    var sceneObjects = GetData().GetArray("m_sceneObjects");
        //    if (sceneObjects.Length == 0)
        //    {
        //        MinBounds = MaxBounds = new Vector3(0, 0, 0);
        //        return;
        //    }

        //    var minBounds = sceneObjects[0].GetSubCollection("m_vMinBounds").ToVector3();
        //    var maxBounds = sceneObjects[0].GetSubCollection("m_vMaxBounds").ToVector3();

        //    for (var i = 1; i < sceneObjects.Length; ++i)
        //    {
        //        var localMin = sceneObjects[i].GetSubCollection("m_vMinBounds").ToVector3();
        //        var localMax = sceneObjects[i].GetSubCollection("m_vMaxBounds").ToVector3();

        //        minBounds.X = System.Math.Min(minBounds.X, localMin.X);
        //        minBounds.Y = System.Math.Min(minBounds.Y, localMin.Y);
        //        minBounds.Z = System.Math.Min(minBounds.Z, localMin.Z);
        //        maxBounds.X = System.Math.Max(maxBounds.X, localMax.X);
        //        maxBounds.Y = System.Math.Max(maxBounds.Y, localMax.Y);
        //        maxBounds.Z = System.Math.Max(maxBounds.Z, localMax.Z);
        //    }

        //    MinBounds = minBounds;
        //    MaxBounds = maxBounds;
        //}
    }
}
