using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace GameEstate.Graphics.OpenGL
{
    public class GLMesh : Mesh
    {
        readonly IOpenGLGraphic _graphic;
        readonly IMeshInfo _mesh;

        public GLMesh(IOpenGLGraphic graphic, IMeshInfo mesh, Dictionary<string, string> skinMaterials = null)
        {
            _graphic = graphic;
            _mesh = mesh;
            BoundingBox = new AABB(mesh.MinBounds, mesh.MaxBounds);

            SetupDrawCalls(mesh, skinMaterials);
        }

        public override void SetRenderMode(string renderMode)
        {
            var drawCalls = DrawCallsOpaque.Union(DrawCallsBlended);

            foreach (var call in drawCalls)
            {
                // Recycle old shader parameters that are not render modes since we are scrapping those anyway
                var parameters = call.Shader.Parameters
                    .Where(kvp => !kvp.Key.StartsWith("renderMode"))
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                if (renderMode != null && call.Shader.RenderModes.Contains(renderMode))
                    parameters.Add($"renderMode_{renderMode}", true);

                call.Shader = _graphic.LoadShader(call.Shader.Name, parameters);
                call.VertexArrayObject = _graphic.MeshBufferCache.GetVertexArrayObject(_mesh.VBIB, call.Shader, call.VertexBuffer.Id, call.IndexBuffer.Id);
            }
        }

        void SetupDrawCalls(IMeshInfo mesh, Dictionary<string, string> skinMaterials)
        {
            var vbib = mesh.VBIB;
            var data = mesh.Data;
            var gpuMeshBuffers = _graphic.MeshBufferCache.GetVertexIndexBuffers(vbib);

            // Prepare drawcalls
            var sceneObjects = data.GetArray("m_sceneObjects");

            foreach (var sceneObject in sceneObjects)
            {
                var objectDrawCalls = sceneObject.GetArray("m_drawCalls");

                foreach (var objectDrawCall in objectDrawCalls)
                {
                    var materialName = objectDrawCall.Get<string>("m_material");

                    if (skinMaterials != null && skinMaterials.ContainsKey(materialName))
                        materialName = skinMaterials[materialName];

                    var material = _graphic.MaterialManager.LoadMaterial(materialName, out var _);
                    var isOverlay = material.Info is IParamMaterialInfo z && z.IntParams.ContainsKey("F_OVERLAY");

                    // Ignore overlays for now
                    if (isOverlay)
                        continue;

                    var shaderArguments = new Dictionary<string, bool>();

                    if (DrawCall.IsCompressedNormalTangent(objectDrawCall))
                        shaderArguments.Add("fulltangent", false);

                    // TODO: Don't pass around so much shit
                    var drawCall = CreateDrawCall(objectDrawCall, vbib, shaderArguments, material);

                    if (drawCall.Material.IsBlended) DrawCallsBlended.Add(drawCall);
                    else DrawCallsOpaque.Add(drawCall);
                }
            }

            //drawCalls = drawCalls.OrderBy(x => x.Material.Parameters.Name).ToList();
        }

        DrawCall<Material> CreateDrawCall(IDictionary<string, object> objectDrawCall, IVBIB vbib, IDictionary<string, bool> shaderArgs, Material material)
        {
            var drawCall = new DrawCall<Material>
            {
                PrimitiveType = (objectDrawCall.TryGetValue("m_nPrimitiveType", out var z) ? (string)z : null) switch
                {
                    "RENDER_PRIM_TRIANGLES" => (int)PrimitiveType.Triangles,
                    _ => throw new Exception($"Unknown PrimitiveType in drawCall! ({(string)z})"),
                },
                Material = material
            };
            // Add shader parameters from material to the shader parameters from the draw call
            var combinedShaderArgs = shaderArgs
                .Concat(material.Info.GetShaderArgs())
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            // Load shader
            drawCall.Shader = _graphic.LoadShader(drawCall.Material.Info.ShaderName, combinedShaderArgs);

            //Bind and validate shader
            GL.UseProgram(drawCall.Shader.Program);

            var indexBufferObject = objectDrawCall.GetSub("m_indexBuffer");

            var indexBuffer = default(DrawCall.DrawBuffer);
            indexBuffer.Id = Convert.ToUInt32(indexBufferObject.Get<object>("m_hBuffer"));
            indexBuffer.Offset = Convert.ToUInt32(indexBufferObject.Get<object>("m_nBindOffsetBytes"));
            drawCall.IndexBuffer = indexBuffer;

            var indexElementSize = vbib.IndexBuffers[(int)drawCall.IndexBuffer.Id].Size;
            //drawCall.BaseVertex = Convert.ToUInt32(objectDrawCall.Get<object>("m_nBaseVertex"));
            //drawCall.VertexCount = Convert.ToUInt32(objectDrawCall.Get<object>("m_nVertexCount"));
            drawCall.StartIndex = Convert.ToUInt32(objectDrawCall.Get<object>("m_nStartIndex")) * indexElementSize;
            drawCall.IndexCount = Convert.ToInt32(objectDrawCall.Get<object>("m_nIndexCount"));

            if (objectDrawCall.ContainsKey("m_vTintColor"))
            {
                var tintColor = objectDrawCall.GetSub("m_vTintColor").ToVector3();
                drawCall.TintColor = new Vector3(tintColor.X, tintColor.Y, tintColor.Z);
            }

            if (!drawCall.Material.Textures.ContainsKey("g_tTintMask"))
                drawCall.Material.Textures.Add("g_tTintMask", _graphic.TextureManager.BuildSolidTexture(1, 1, 1f, 1f, 1f, 1f));

            if (!drawCall.Material.Textures.ContainsKey("g_tNormal"))
                drawCall.Material.Textures.Add("g_tNormal", _graphic.TextureManager.BuildSolidTexture(1, 1, 0.5f, 1f, 0.5f, 1f));

            if (indexElementSize == 2) drawCall.IndexType = (int)DrawElementsType.UnsignedShort; // shopkeeper_vr
            else if (indexElementSize == 4) drawCall.IndexType = (int)DrawElementsType.UnsignedInt; // glados
            else throw new Exception("Unsupported index type");

            var m_vertexBuffers = objectDrawCall.GetSub("m_vertexBuffers");
            var m_vertexBuffer = m_vertexBuffers.GetSub("0"); // TODO: Not just 0

            var vertexBuffer = default(DrawCall.DrawBuffer);
            vertexBuffer.Id = Convert.ToUInt32(m_vertexBuffer.Get<object>("m_hBuffer"));
            vertexBuffer.Offset = Convert.ToUInt32(m_vertexBuffer.Get<object>("m_nBindOffsetBytes"));
            drawCall.VertexBuffer = vertexBuffer;

            drawCall.VertexArrayObject = _graphic.MeshBufferCache.GetVertexArrayObject(vbib, drawCall.Shader, drawCall.VertexBuffer.Id, drawCall.IndexBuffer.Id);

            return drawCall;
        }
    }
}
