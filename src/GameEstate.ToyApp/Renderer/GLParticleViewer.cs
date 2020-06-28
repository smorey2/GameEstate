using System;
using System.Collections.Generic;
using System.Numerics;
//using static GUI.Controls.GLViewerControl;

namespace GameEstate.Toy.Renderer
{
    /// <summary>
    /// GL Render control with particle controls (control points? particle counts?).
    /// Renders a list of ParticleRenderers.
    /// </summary>
#pragma warning disable CA1001 // Types that own disposable fields should be disposable
    public class GLParticleViewer
#pragma warning restore CA1001 // Types that own disposable fields should be disposable
    {
        ICollection<ParticleRenderer.ParticleRenderer> Renderers { get; } = new HashSet<ParticleRenderer.ParticleRenderer>();

        public event EventHandler Load;

        public Control Control => _viewerControl;

        readonly GLViewerControl _viewerControl;
        readonly GuiContext _guiContext;

        ParticleGrid particleGrid;

        public GLParticleViewer(GuiContext guiContext)
        {
            _guiContext = guiContext;

            _viewerControl = new GLViewerControl();

            _viewerControl.GLLoad += OnLoad;
        }

        void OnLoad(object sender, EventArgs e)
        {
            particleGrid = new ParticleGrid(20, 5, _guiContext);

            _viewerControl.Camera.SetViewportSize(_viewerControl.GLControl.Width, _viewerControl.GLControl.Height);
            _viewerControl.Camera.SetLocation(new Vector3(200));
            _viewerControl.Camera.LookAt(new Vector3(0));

            Load?.Invoke(this, e);

            _viewerControl.GLPaint += OnPaint;
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

        public void AddRenderer(ParticleRenderer.ParticleRenderer renderer) => Renderers.Add(renderer);
    }
}
