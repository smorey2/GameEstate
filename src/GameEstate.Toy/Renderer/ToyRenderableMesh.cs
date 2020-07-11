using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GameEstate.Graphics;
using GameEstate.Graphics.Scene;
using GameEstate.Toy.Models;
using OpenTK.Graphics.OpenGL;

namespace GameEstate.Toy.Renderer
{
    public class ToyRenderableMesh : RenderableMesh
    {
        public AABB BoundingBox { get; }
        public Vector4 Tint { get; set; } = Vector4.One;

        readonly GuiContext _guiContext;
        //public List<DrawCall> DrawCallsOpaque { get; } = new List<DrawCall>();
        //public List<DrawCall> DrawCallsBlended { get; } = new List<DrawCall>();
        public int? AnimationTexture { get; private set; }
        public int AnimationTextureSize { get; private set; }

        public float Time { get; private set; } = 0f;

        Mesh _mesh;

        public ToyRenderableMesh(Mesh mesh, GuiContext guiContext, Dictionary<string, string> skinMaterials = null)
        {
            _guiContext = guiContext;
            _mesh = mesh;
            BoundingBox = new AABB(mesh.MinBounds, mesh.MaxBounds);

            SetupDrawCalls(mesh, skinMaterials);
        }

        public IEnumerable<string> GetSupportedRenderModes() => DrawCallsOpaque
            .SelectMany(drawCall => drawCall.Shader.RenderModes)
            .Union(DrawCallsBlended.SelectMany(drawCall => drawCall.Shader.RenderModes))
            .Distinct();

        public void SetRenderMode(string renderMode)
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

                call.Shader = _guiContext.ShaderLoader.LoadShader(call.Shader.Name, parameters);
                call.VertexArrayObject = _guiContext.MeshBufferCache.GetVertexArrayObject(_mesh.VBIB, call.Shader, call.VertexBuffer.Id, call.IndexBuffer.Id);
            }
        }

        public void SetAnimationTexture(int? texture, int animationTextureSize)
        {
            AnimationTexture = texture;
            AnimationTextureSize = animationTextureSize;
        }

        public void Update(float timeStep) =>
            Time += timeStep;

        void SetupDrawCalls(Mesh mesh, Dictionary<string, string> skinMaterials)
        {
            var vbib = mesh.VBIB;
            var data = mesh;
            var gpuMeshBuffers = _guiContext.MeshBufferCache.GetVertexIndexBuffers(vbib);

            // Prepare drawcalls
            var sceneObjects = data.Get<object[]>("m_sceneObjects");

            foreach (var sceneObject in sceneObjects)
            {
                var objectDrawCalls = sceneObject.GetArray("m_drawCalls");

                foreach (var objectDrawCall in objectDrawCalls)
                {
                    var materialName = objectDrawCall.GetProperty<string>("m_material");

                    if (skinMaterials != null && skinMaterials.ContainsKey(materialName))
                        materialName = skinMaterials[materialName];

                    var material = _guiContext.MaterialLoader.GetMaterial(materialName);
                    var isOverlay = material.Material.IntParams.ContainsKey("F_OVERLAY");

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

        DrawCall CreateDrawCall(IDictionary<string, object> objectDrawCall, VBIB vbib, IDictionary<string, bool> shaderArguments, RenderMaterial material)
        {
            var drawCall = new DrawCall
            {
                PrimitiveType = (objectDrawCall.TryGetValue("m_nPrimitiveType", out var z) ? (string)z : null) switch
                {
                    "RENDER_PRIM_TRIANGLES" => PrimitiveType.Triangles,
                    _ => throw new Exception($"Unknown PrimitiveType in drawCall! ({(string)z})"),
                },
                Material = material
            };
            // Add shader parameters from material to the shader parameters from the draw call
            var combinedShaderParameters = shaderArguments
                .Concat(material.Material.GetShaderArguments())
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            // Load shader
            drawCall.Shader = _guiContext.ShaderLoader.LoadShader(drawCall.Material.Material.ShaderName, combinedShaderParameters);

            //Bind and validate shader
            GL.UseProgram(drawCall.Shader.Program);

            var indexBufferObject = objectDrawCall.GetSub("m_indexBuffer");

            var indexBuffer = default(DrawBuffer);
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
                drawCall.TintColor = new OpenTK.Vector3(tintColor.X, tintColor.Y, tintColor.Z);
            }

            if (!drawCall.Material.Textures.ContainsKey("g_tTintMask"))
                drawCall.Material.Textures.Add("g_tTintMask", MaterialLoader.CreateSolidTexture(1f, 1f, 1f));

            if (!drawCall.Material.Textures.ContainsKey("g_tNormal"))
                drawCall.Material.Textures.Add("g_tNormal", MaterialLoader.CreateSolidTexture(0.5f, 1f, 0.5f));

            if (indexElementSize == 2) drawCall.IndexType = DrawElementsType.UnsignedShort; // shopkeeper_vr
            else if (indexElementSize == 4) drawCall.IndexType = DrawElementsType.UnsignedInt; // glados
            else throw new Exception("Unsupported index type");

            var m_vertexBuffers = objectDrawCall.GetSub("m_vertexBuffers");
            var m_vertexBuffer = m_vertexBuffers.GetSub("0"); // TODO: Not just 0

            var vertexBuffer = default(DrawBuffer);
            vertexBuffer.Id = Convert.ToUInt32(m_vertexBuffer.Get<object>("m_hBuffer"));
            vertexBuffer.Offset = Convert.ToUInt32(m_vertexBuffer.Get<object>("m_nBindOffsetBytes"));
            drawCall.VertexBuffer = vertexBuffer;

            drawCall.VertexArrayObject = _guiContext.MeshBufferCache.GetVertexArrayObject(vbib, drawCall.Shader, drawCall.VertexBuffer.Id, drawCall.IndexBuffer.Id);

            return drawCall;
        }
    }
}
