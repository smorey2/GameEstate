using GameEstate.Graphics;
using GameEstate.Graphics.Controls;
using GameEstate.Graphics.OpenGL.Renderers;
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
            get => GetValue(SourceProperty) as object;
            set => SetValue(SourceProperty, value);
        }

        HashSet<ParticleRenderer> Renderers { get; } = new HashSet<ParticleRenderer>();

        void OnProperty()
        {
            if (Graphic == null || Source == null)
                return;
            particleGrid = new ParticleGridRenderer(20, 5, null);

            Camera.SetViewportSize((int)ActualWidth, (int)ActualHeight); //: HandleResize()
            Camera.SetLocation(new Vector3(200));
            Camera.LookAt(new Vector3(0));
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
