//using GameEstate.Toy.Renderer;
//using OpenTK;
//using OpenTK.Graphics;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Windows;
//using System.Windows.Controls;

//namespace GameEstate.Toy.Controls
//{
//    public class GLBaseControl : Control
//    {
//        public GLControl GLControl { get; }

//        public class RenderEventArgs
//        {
//            public float FrameTime { get; set; }
//            public Camera Camera { get; set; }
//        }

//        public Camera Camera { get; } = new Camera();

//        public event EventHandler<RenderEventArgs> GLPaint;
//        public event EventHandler GLLoad;

//        readonly Stopwatch _stopwatch = new Stopwatch();

//        static bool _hasCheckedOpenGL;

//        public GLBaseControl()
//        {
//            //Stretch = Stretch.Fill;

//            // Initialize GL control
//#if DEBUG
//            GLControl = new GLControl(new GraphicsMode(32, 24, 0, 8), 3, 3, GraphicsContextFlags.Debug);
//#else
//            GLControl = new GLControl(new GraphicsMode(32, 24, 0, 8), 3, 3, GraphicsContextFlags.Default);
//#endif
//            GLControl.Loaded += OnLoad;
//            GLControl.Paint += OnPaint;
//            GLControl.Resize += OnResize;
//            GLControl.MouseEnter += OnMouseEnter;
//            GLControl.MouseLeave += OnMouseLeave;
//            GLControl.GotFocus += OnGotFocus;
//            GLControl.VisibleChanged += OnVisibleChanged;
//            GLControl.Disposed += OnDisposed;

//            GLControl.Dock = DockStyle.Fill;
//            glControlContainer.Controls.Add(GLControl);
//        }

//        void OnDisposed(object sender, EventArgs e)
//        {
//            GLControl.Loaded -= OnLoad;
//            GLControl.Paint -= OnPaint;
//            GLControl.Resize -= OnResize;
//            GLControl.MouseEnter -= OnMouseEnter;
//            GLControl.MouseLeave -= OnMouseLeave;
//            GLControl.GotFocus -= OnGotFocus;
//            GLControl.VisibleChanged -= OnVisibleChanged;
//            GLControl.Disposed -= OnDisposed;
//        }

//        void OnVisibleChanged(object sender, EventArgs e)
//        {
//            if (GLControl.Visible)
//            {
//                GLControl.Focus();
//                HandleResize();
//            }
//        }

//        void OnMouseLeave(object sender, EventArgs e)
//        {
//            Camera.MouseOverRenderArea = false;
//        }

//        void OnMouseEnter(object sender, EventArgs e)
//        {
//            Camera.MouseOverRenderArea = true;
//        }

//        void OnLoad(object sender, EventArgs e)
//        {
//            GLControl.MakeCurrent();

//            CheckOpenGL();

//            _stopwatch.Start();

//            GLLoad?.Invoke(this, e);

//            HandleResize();
//            Draw();
//        }

//        void OnPaint(object sender, EventArgs e)
//        {
//            Draw();
//        }

//        void Draw()
//        {
//            if (GLControl.Visible)
//            {
//                var frameTime = _stopwatch.ElapsedMilliseconds / 1000f;
//                _stopwatch.Restart();

//                Camera.Tick(frameTime);
//                Camera.HandleInput(Mouse.GetState(), Keyboard.GetState());

//                SetFps(1f / frameTime);

//                GL.ClearColor(Settings.BackgroundColor);
//                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

//                GLPaint?.Invoke(this, new RenderEventArgs { FrameTime = frameTime, Camera = Camera });

//                GLControl.SwapBuffers();
//                GLControl.Invalidate();
//            }
//        }

//        private void OnResize(object sender, EventArgs e)
//        {
//            HandleResize();
//            Draw();
//        }

//        void HandleResize()
//        {
//            Camera.SetViewportSize(GLControl.Width, GLControl.Height);
//            RecalculatePositions();
//        }

//        void OnGotFocus(object sender, EventArgs e)
//        {
//            GLControl.MakeCurrent();
//            HandleResize();
//            Draw();
//        }

//        static void CheckOpenGL()
//        {
//            if (_hasCheckedOpenGL)
//                return;

//            _hasCheckedOpenGL = true;

//            Console.WriteLine("OpenGL version: " + GL.GetString(StringName.Version));
//            Console.WriteLine("OpenGL vendor: " + GL.GetString(StringName.Vendor));
//            Console.WriteLine("GLSL version: " + GL.GetString(StringName.ShadingLanguageVersion));

//            var extensions = new HashSet<string>();
//            var count = GL.GetInteger(GetPName.NumExtensions);
//            for (var i = 0; i < count; i++)
//            {
//                var extension = GL.GetString(StringNameIndexed.Extensions, i);
//                if (!extensions.Contains(extension))
//                {
//                    extensions.Add(extension);
//                }
//            }

//            if (extensions.Contains("GL_EXT_texture_filter_anisotropic"))
//            {
//                MaterialLoader.MaxTextureMaxAnisotropy = GL.GetInteger((GetPName)ExtTextureFilterAnisotropic.MaxTextureMaxAnisotropyExt);
//            }
//            else
//            {
//                Console.Error.WriteLine("GL_EXT_texture_filter_anisotropic is not supported");
//            }
//        }
//    }
//}
