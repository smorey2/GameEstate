//using System;
//using GameEstate.Graphics;
//using GameEstate.Graphics.OpenGL;
//using OpenTK;
//using OpenTK.Graphics.OpenGL;

//namespace GameEstate.Toy.Renderer
//{
//    public class MaterialRenderer : IRenderer
//    {
//        readonly RenderMaterial material;
//        readonly Shader shader;
//        readonly int quadVao;

//        public AABB BoundingBox => new AABB(-1, -1, -1, 1, 1, 1);

//        public MaterialRenderer(RenderMaterial renderMaterial)
//        {
//            material = renderMaterial;
//            shader = ShaderLoader.LoadPlaneShader(material.Material.ShaderName, material.Material.GetShaderArguments());
//            quadVao = SetupQuadBuffer();
//        }

//        int SetupQuadBuffer()
//        {
//            GL.UseProgram(shader.Program);

//            // Create and bind VAO
//            var vao = GL.GenVertexArray();
//            GL.BindVertexArray(vao);

//            var vbo = GL.GenBuffer();
//            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

//            var vertices = new[]
//            {
//                // position       ; normal          ; texcoord  ; tangent
//                -1.0f, -1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, 0.0f, 0.0f,
//                // position      ; normal          ; texcoord  ; tangent
//                -1.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f,
//                // position      ; normal          ; texcoord  ; tangent
//                1.0f, -1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f,
//                // position     ; normal          ; texcoord  ; tangent
//                1.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 0.0f, 1.0f, 0.0f, 0.0f,
//            };

//            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

//            GL.EnableVertexAttribArray(0);

//            var stride = sizeof(float) * 11;

//            var positionAttributeLocation = GL.GetAttribLocation(shader.Program, "vPOSITION");
//            GL.EnableVertexAttribArray(positionAttributeLocation);
//            GL.VertexAttribPointer(positionAttributeLocation, 3, VertexAttribPointerType.Float, false, stride, 0);

//            var normalAttributeLocation = GL.GetAttribLocation(shader.Program, "vNORMAL");
//            GL.EnableVertexAttribArray(normalAttributeLocation);
//            GL.VertexAttribPointer(normalAttributeLocation, 3, VertexAttribPointerType.Float, false, stride, sizeof(float) * 3);

//            var texCoordAttributeLocation = GL.GetAttribLocation(shader.Program, "vTEXCOORD");
//            GL.EnableVertexAttribArray(texCoordAttributeLocation);
//            GL.VertexAttribPointer(texCoordAttributeLocation, 2, VertexAttribPointerType.Float, false, stride, sizeof(float) * 6);

//            var tangentAttributeLocation = GL.GetAttribLocation(shader.Program, "vTANGENT");
//            GL.EnableVertexAttribArray(tangentAttributeLocation);
//            GL.VertexAttribPointer(tangentAttributeLocation, 3, VertexAttribPointerType.Float, false, stride, sizeof(float) * 8);

//            GL.BindVertexArray(0); // Unbind VAO

//            return vao;
//        }

//        public void Render(Camera camera, RenderPass renderPass)
//        {
//            GL.UseProgram(shader.Program);
//            GL.BindVertexArray(quadVao);
//            GL.EnableVertexAttribArray(0);

//            var uniformLocation = shader.GetUniformLocation("m_vTintColorSceneObject");
//            if (uniformLocation > -1)
//                GL.Uniform4(uniformLocation, Vector4.One);

//            uniformLocation = shader.GetUniformLocation("m_vTintColorDrawCall");
//            if (uniformLocation > -1)
//                GL.Uniform3(uniformLocation, Vector3.One);

//            var identity = Matrix4.Identity;

//            uniformLocation = shader.GetUniformLocation("projection");
//            GL.UniformMatrix4(uniformLocation, false, ref identity);

//            uniformLocation = shader.GetUniformLocation("modelview");
//            GL.UniformMatrix4(uniformLocation, false, ref identity);

//            material.Render(shader);

//            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);

//            material.PostRender();

//            GL.BindVertexArray(0);
//            GL.UseProgram(0);
//        }

//        public void Update(float frameTime) => throw new NotImplementedException();
//    }
//}
