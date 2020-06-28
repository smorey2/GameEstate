using GameEstate.Core;
using GameEstate.Toy.Core;
using GameEstate.Toy.Models;
using System.Collections.Generic;

namespace GameEstate.Toy.Blocks
{
    /// <summary>
    /// "VBIB" block.
    /// </summary>
    public class VBIB : Block
    {
        public override BlockType Type => BlockType.VBIB;

        public List<VertexBuffer> VertexBuffers { get; }
        public List<IndexBuffer> IndexBuffers { get; }

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

        public VBIB()
        {
            VertexBuffers = new List<VertexBuffer>();
            IndexBuffers = new List<IndexBuffer>();
        }

        public override void WriteText(IndentedTextWriter writer)
        {
            writer.WriteLine("Vertex buffers:");

            foreach (var vertexBuffer in VertexBuffers)
            {
                writer.WriteLine($"Count: {vertexBuffer.Count}");
                writer.WriteLine($"Size: {vertexBuffer.Size}");

                for (var i = 0; i < vertexBuffer.Attributes.Count; i++)
                {
                    var vertexAttribute = vertexBuffer.Attributes[i];
                    writer.WriteLine($"Attribute[{i}].Name = {vertexAttribute.Name}");
                    writer.WriteLine($"Attribute[{i}].Offset = {vertexAttribute.Offset}");
                    writer.WriteLine($"Attribute[{i}].Type = {vertexAttribute.Type}");
                }

                writer.WriteLine();
            }

            writer.WriteLine();
            writer.WriteLine("Index buffers:");

            foreach (var indexBuffer in IndexBuffers)
            {
                writer.WriteLine($"Count: {indexBuffer.Count}");
                writer.WriteLine($"Size: {indexBuffer.Size}");
                writer.WriteLine();
            }
        }
    }
}
