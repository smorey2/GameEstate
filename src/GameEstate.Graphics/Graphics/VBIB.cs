using GameEstate.Core;
using GameEstate.Graphics.DirectX;
using System.Collections.Generic;

namespace GameEstate.Graphics
{
    /// <summary>
    /// VBIB
    /// </summary>
    public class VBIB
    {
        public List<VertexBuffer> VertexBuffers { get; } = new List<VertexBuffer>();
        public List<IndexBuffer> IndexBuffers { get; } = new List<IndexBuffer>();

#pragma warning disable CA1051 // Do not declare visible instance fields
        public struct VertexBuffer
        {
            public uint Count;
            public uint Size;
            public List<VertexAttribute> Attributes;
            public byte[] Buffer;
        }

        public struct VertexAttribute
        {
            public string Name;
            public DXGI_FORMAT Type;
            public uint Offset;
        }

        public struct IndexBuffer
        {
            public uint Count;
            public uint Size;
            public byte[] Buffer;
        }
#pragma warning restore CA1051 // Do not declare visible instance fields

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            using (var w = new IndentedTextWriter())
            {
                WriteText(w);
                return w.ToString();
            }
        }

        public void WriteText(IndentedTextWriter w)
        {
            w.WriteLine("Vertex buffers:");
            foreach (var vertexBuffer in VertexBuffers)
            {
                w.WriteLine($"Count: {vertexBuffer.Count}");
                w.WriteLine($"Size: {vertexBuffer.Size}");
                for (var i = 0; i < vertexBuffer.Attributes.Count; i++)
                {
                    var vertexAttribute = vertexBuffer.Attributes[i];
                    w.WriteLine($"Attribute[{i}].Name = {vertexAttribute.Name}");
                    w.WriteLine($"Attribute[{i}].Offset = {vertexAttribute.Offset}");
                    w.WriteLine($"Attribute[{i}].Type = {vertexAttribute.Type}");
                }
                w.WriteLine();
            }
            w.WriteLine();
            w.WriteLine("Index buffers:");
            foreach (var indexBuffer in IndexBuffers)
            {
                w.WriteLine($"Count: {indexBuffer.Count}");
                w.WriteLine($"Size: {indexBuffer.Size}");
                w.WriteLine();
            }
        }
    }
}
