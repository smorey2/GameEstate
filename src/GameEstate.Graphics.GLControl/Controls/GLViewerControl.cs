﻿using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace GameEstate.Graphics.Controls
{
    public class GLViewerControl : GLControl
    {
        readonly Stopwatch _stopwatch = new Stopwatch();
        static bool _hasCheckedOpenGL;

        public class RenderEventArgs
        {
            public float FrameTime { get; set; }
            public Camera Camera { get; set; }
        }

        public event EventHandler<RenderEventArgs> GLPaint;
        public event EventHandler GLLoad;
        readonly DispatcherTimer _timer = new DispatcherTimer() { Interval = new TimeSpan(10) };

        public GLViewerControl()
        {
            if (!IsInDesignMode) ConsoleManager.Show();
        }

        void OnTimerElapsed(object sender, EventArgs e) => InvalidateVisual();

        void SetFps(double fps) => Console.WriteLine($"FPS: {Math.Round(fps)}");

        public DebugCamera Camera { get; } = new DebugCamera();

        void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!HasValidContext || (bool)e.NewValue != true)
                return;
            Focus();
            HandleResize();
        }

        protected override void OnMouseEnter(MouseEventArgs e) { Console.WriteLine("Enter"); Camera.MouseOverRenderArea = true; }

        protected override void OnMouseLeave(MouseEventArgs e) { Console.WriteLine("Leave"); Camera.MouseOverRenderArea = false; }

        protected override void AttachHandle(HandleRef hwnd)
        {
            base.AttachHandle(hwnd);
            IsVisibleChanged += OnIsVisibleChanged;
            _timer.Tick += OnTimerElapsed;
            _timer.Start();
            if (!HasValidContext)
                return;

            MakeCurrent();

            CheckOpenGL();

            _stopwatch.Start();
            GL.Enable(EnableCap.DepthTest);
            GLLoad?.Invoke(this, null);

            HandleResize();
            Draw();
        }

        protected override void DestroyHandle(HandleRef hwnd)
        {
            IsVisibleChanged -= OnIsVisibleChanged;
            _timer.Tick -= OnTimerElapsed;
            _timer.Stop();
            base.DestroyHandle(hwnd);
        }

        protected override void OnRender(DrawingContext drawingContext) { if (HasValidContext) Draw(); else base.OnRender(drawingContext); }

        void Draw()
        {
            if (Visibility != Visibility.Visible)
                return;

            var frameTime = _stopwatch.ElapsedMilliseconds / 1000f;
            _stopwatch.Restart();

            Camera.Tick(frameTime);
            Camera.HandleInput(OpenTK.Input.Mouse.GetState(), OpenTK.Input.Keyboard.GetState());

            //SetFps(1f / frameTime);

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GLPaint?.Invoke(this, new RenderEventArgs { FrameTime = frameTime, Camera = Camera });

            SwapBuffers();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) { if (HasValidContext) { HandleResize(); Draw(); } }

        public void RecalculatePositions()
        {
        }

        protected void HandleResize()
        {
            if (ActualWidth <= 0 || ActualHeight <= 0)
                return;

            //GL.Viewport(0, 0, (int)ActualWidth, (int)ActualHeight);
            //var aspectRatio = (float)ActualWidth / (float)ActualHeight;
            //var projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1.0f, 40000.0f); //64f
            //GL.MatrixMode(MatrixMode.Projection);
            //GL.LoadMatrix(ref projection);

            Camera.SetViewportSize((int)ActualWidth, (int)ActualHeight);
            RecalculatePositions();
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            MakeCurrent();
            HandleResize();
            Draw();
        }

        void CheckOpenGL()
        {
            if (_hasCheckedOpenGL)
                return;
            _hasCheckedOpenGL = true;

            Console.WriteLine($"OpenGL version: {GL.GetString(StringName.Version)}");
            Console.WriteLine($"OpenGL vendor: {GL.GetString(StringName.Vendor)}");
            Console.WriteLine($"GLSL version: {GL.GetString(StringName.ShadingLanguageVersion)}");

            var extensions = new HashSet<string>();
            var count = GL.GetInteger(GetPName.NumExtensions);
            for (var i = 0; i < count; i++)
            {
                var extension = GL.GetString(StringNameIndexed.Extensions, i);
                if (!extensions.Contains(extension))
                    extensions.Add(extension);
            }

            if (extensions.Contains("GL_EXT_texture_filter_anisotropic"))
            {
                var maxTextureMaxAnisotropy = GL.GetInteger((GetPName)ExtTextureFilterAnisotropic.MaxTextureMaxAnisotropyExt);
                Console.WriteLine($"MaxTextureMaxAnisotropyExt: {maxTextureMaxAnisotropy}");
            }
            else
                Console.Error.WriteLine("GL_EXT_texture_filter_anisotropic is not supported");
        }
    }
}
