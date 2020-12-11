using GameEstate.Core;
using GameEstate.Graphics;
using GameEstate.Graphics.Controls;
using GameEstate.Graphics.OpenGL.Renderers;
using GameEstate.Graphics.ParticleSystem;
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
            GLPaint += OnPaint;
            Unloaded += (a, b) =>
            {
                GLPaint -= OnPaint;
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public static readonly DependencyProperty GraphicProperty = DependencyProperty.Register(nameof(Graphic), typeof(object), typeof(GLParticleViewer),
            new PropertyMetadata((d, e) => (d as GLParticleViewer).OnProperty()));
        public IEstateGraphic Graphic
        {
            get => GetValue(GraphicProperty) as IEstateGraphic;
            set => SetValue(GraphicProperty, value);
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), typeof(object), typeof(GLParticleViewer),
            new PropertyMetadata((d, e) => (d as GLParticleViewer).OnProperty()));
        public object Source
        {
            get => GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        HashSet<ParticleRenderer> Renderers { get; } = new HashSet<ParticleRenderer>();

        void OnProperty()
        {
            if (Graphic == null || Source == null)
                return;
            var graphic = Graphic as IOpenGLGraphic;
            var source = Source is IParticleSystemInfo z ? z
                : Source is IRedirected<IParticleSystemInfo> y ? y.Value
                : null;
            if (source == null)
                return;

            particleGrid = new ParticleGridRenderer(20, 5, graphic);
            Camera.SetViewportSize((int)ActualWidth, (int)ActualHeight);
            Camera.SetLocation(new Vector3(200));
            Camera.LookAt(new Vector3(0));

            Renderers.Add(new ParticleRenderer(graphic, source));
        }

        void OnPaint(object sender, RenderEventArgs e)
        {
            particleGrid?.Render(e.Camera, RenderPass.Both);
            foreach (var renderer in Renderers)
            {
                renderer.Update(e.FrameTime);
                renderer.Render(e.Camera, RenderPass.Both);
            }
        }
    }
}
