using System.Collections.Generic;

namespace GameEstate.Graphics
{
    /// <summary>
    /// IVBIB
    /// </summary>
    public interface IVBIB
    {
        List<VertexBuffer> VertexBuffers { get; }
        List<IndexBuffer> IndexBuffers { get; }
    }
}
