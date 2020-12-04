using GameEstate.Graphics;
using GameEstate.Graphics.Controls;
using GameEstate.Graphics.OpenGL.Renderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows;

namespace GameEstate.Explorer.View
{
    public class GLParticleViewer : GLViewerControl
    {
        ParticleGridRenderer particleGrid;
       
        public GLParticleViewer()
        {
            GLLoad += OnLoad;
            Unloaded += (a, b) =>
            {
                GLLoad -= OnLoad;
                GLPaint -= OnPaint;
            };
        }

        void OnLoad(object sender, EventArgs e)
        {
            particleGrid = new ParticleGridRenderer(20, 5, null);

            Camera.SetViewportSize((int)Width, (int)Height);
            Camera.SetLocation(new Vector3(200));
            Camera.LookAt(new Vector3(0));

            Load?.Invoke(this, e);
            GLPaint += OnPaint;
        }

        void OnPaint(object sender, RenderEventArgs e)
        {
            particleGrid.Render(e.Camera, RenderPass.Both);
            foreach (var renderer in Renderers)
            {
                renderer.Update(e.FrameTime);
                renderer.Render(e.Camera, RenderPass.Both);
            }
        }
        
        public event EventHandler Load;

        HashSet<ParticleRenderer> Renderers { get; } = new HashSet<ParticleRenderer>();

        public void AddRenderer(ParticleRenderer renderer) => Renderers.Add(renderer);

        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), typeof(object), typeof(GLParticleViewer),
            new PropertyMetadata((d, e) => (d as GLParticleViewer).LoadSource()));

        public object Source
        {
            get => GetValue(SourceProperty) as object;
            set => SetValue(SourceProperty, value);
        }

        void LoadSource()
        {
        }
    }
}
