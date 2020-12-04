using GameEstate.Graphics;
using GameEstate.Graphics.Controls;
using GameEstate.Graphics.OpenGL.Renderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace GameEstate.Explorer.View
{
    public class GLMaterialViewer : GLViewerControl
    {
        public GLMaterialViewer()
        {
            GLLoad += OnLoad;
            Unloaded += (a, b) =>
            {
                GLLoad -= OnLoad;
                GLPaint -= OnPaint;
            };
        }

        public event EventHandler Load;

        void OnLoad(object sender, EventArgs e)
        {
            Load?.Invoke(this, e);
            GLPaint += OnPaint;
        }

        void OnPaint(object sender, RenderEventArgs e)
        {
            foreach (var renderer in Renderers)
                renderer.Render(e.Camera, RenderPass.Both);
        }

        HashSet<MaterialRenderer> Renderers { get; } = new HashSet<MaterialRenderer>();

        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), typeof(object), typeof(GLMaterialViewer),
            new PropertyMetadata((d, e) => (d as GLMaterialViewer).LoadSource()));

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
