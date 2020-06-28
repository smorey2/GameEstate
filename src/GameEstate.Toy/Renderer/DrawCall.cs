using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace GameEstate.Toy.Renderer
{
    public struct DrawBuffer
    {
        public uint Id;
        public uint Offset;
    }

    public class DrawCall
    {
        public PrimitiveType PrimitiveType { get; set; }
        public Shader Shader { get; set; }
        //public uint BaseVertex { get; set; }
        //public uint VertexCount { get; set; }
        public uint StartIndex { get; set; }
        public int IndexCount { get; set; }
        //public uint InstanceIndex { get; set; }   //TODO
        //public uint InstanceCount { get; set; }   //TODO
        //public float UvDensity { get; set; }     //TODO
        //public string Flags { get; set; }        //TODO
        public Vector3 TintColor { get; set; } = Vector3.One;
        public RenderMaterial Material { get; set; }
        public uint VertexArrayObject { get; set; }
        public DrawBuffer VertexBuffer { get; set; }
        public DrawElementsType IndexType { get; set; }
        public DrawBuffer IndexBuffer { get; set; }

        public static bool IsCompressedNormalTangent(IDictionary<string, object> drawCall)
        {
            if (drawCall.TryGetValue("m_bUseCompressedNormalTangent", out var z))
                return (bool)z;

            if (drawCall.TryGetValue("m_nFlags", out var z2))
            {
                var flags = z;
                switch (flags)
                {
                    case string flagsString: return flagsString.Contains("MESH_DRAW_FLAGS_USE_COMPRESSED_NORMAL_TANGENT");
                    case long flagsLong: return (flagsLong & 2) == 2; // TODO: enum
                }
            }

            return false;
        }
    }
}
