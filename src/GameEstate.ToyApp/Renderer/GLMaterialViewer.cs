using System;
using System.Collections.Generic;
using System.Windows;
//using static GUI.Controls.GLViewerControl;

namespace GameEstate.Toy.Renderer
{
    /// <summary>
    /// GL Render control with material controls (render modes maybe at some point?).
    /// Renders a list of MatarialRenderers.
    /// </summary>
#pragma warning disable CA1001 // Types that own disposable fields should be disposable
    public class GLMaterialViewer
#pragma warning restore CA1001 // Types that own disposable fields should be disposable
    {
        ICollection<MaterialRenderer> Renderers { get; } = new HashSet<MaterialRenderer>();

        public event EventHandler Load;

        public FrameworkElement Control => _viewerControl;

        readonly GLViewerControl _viewerControl;

        public GLMaterialViewer()
        {
            _viewerControl = new GLViewerControl();

            _viewerControl.GLLoad += OnLoad;
        }

        void OnLoad(object sender, EventArgs e)
        {
            Load?.Invoke(this, e);

            _viewerControl.GLPaint += OnPaint;
        }

        void OnPaint(object sender, RenderEventArgs e)
        {
            foreach (var renderer in Renderers)
                renderer.Render(e.Camera, RenderPass.Both);
        }

        public void AddRenderer(MaterialRenderer renderer) => Renderers.Add(renderer);
    }
}
