//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Numerics;
//using GameEstate.Graphics;
//using GameEstate.Graphics.Controls;
//using GameEstate.Graphics.OpenGL;
//using GameEstate.Graphics.OpenGL.Renderers;
//using OpenTK.Graphics.OpenGL;

//namespace GameEstate.Explorer.View
//{
//    public abstract class GLSceneViewer : GLViewerControl
//    {
//        public Scene Scene { get; }
//        public Scene SkyboxScene { get; protected set; }

//        public bool ShowBaseGrid { get; set; } = true;
//        public bool ShowSkybox { get; set; } = true;

//        protected float SkyboxScale { get; set; } = 1.0f;
//        protected Vector3 SkyboxOrigin { get; set; } = Vector3.Zero;

//        bool _showStaticOctree = false;
//        bool _showDynamicOctree = false;
//        Frustum _lockedCullFrustum;

//        ComboBox _renderModeComboBox;
//        ParticleGridRenderer _baseGrid;
//        Camera _skyboxCamera = new Camera();
//        OctreeDebugRenderer<SceneNode> _staticOctreeRenderer;
//        OctreeDebugRenderer<SceneNode> _dynamicOctreeRenderer;

//        protected GLSceneViewer(GuiContext guiContext, Frustum cullFrustum)
//        {
//            Scene = new Scene(guiContext);
//            _lockedCullFrustum = cullFrustum;

//            InitializeControl();
//            AddCheckBox("Show Grid", ShowBaseGrid, (v) => ShowBaseGrid = v);
//            GLLoad += OnLoad;
//        }
//        protected GLSceneViewer(GuiContext guiContext)
//        {
//            Scene = new Scene(guiContext);
//            InitializeControl();
//            AddCheckBox("Show Static Octree", _showStaticOctree, (v) => _showStaticOctree = v);
//            AddCheckBox("Show Dynamic Octree", _showDynamicOctree, (v) => _showDynamicOctree = v);
//            AddCheckBox("Lock Cull Frustum", false, (v) => { _lockedCullFrustum = v ? Scene.MainCamera.ViewFrustum.Clone() : null; });
//            GLLoad += OnLoad;
//        }

//        protected abstract void InitializeControl();

//        protected abstract void LoadScene();

//        void OnLoad(object sender, EventArgs e)
//        {
//            _baseGrid = new ParticleGridRenderer(20, 5, GuiContext);

//            Camera.SetViewportSize((int)Width, (int)Height);
//            Camera.SetLocation(new Vector3(256));
//            Camera.LookAt(new Vector3(0));

//            LoadScene();

//            if (Scene.AllNodes.Any())
//            {
//                var bbox = Scene.AllNodes.First().BoundingBox;
//                var location = new Vector3(bbox.Max.Z, 0, bbox.Max.Z) * 1.5f;

//                Camera.SetLocation(location);
//                Camera.LookAt(bbox.Center);
//            }

//            _staticOctreeRenderer = new OctreeDebugRenderer<SceneNode>(Scene.StaticOctree, Scene.GuiContext, false);
//            _dynamicOctreeRenderer = new OctreeDebugRenderer<SceneNode>(Scene.DynamicOctree, Scene.GuiContext, true);

//            if (_renderModeComboBox != null)
//            {
//                var supportedRenderModes = Scene.AllNodes
//                    .SelectMany(r => r.GetSupportedRenderModes())
//                    .Distinct();
//                SetAvailableRenderModes(supportedRenderModes);
//            }

//            GLLoad -= OnLoad;
//            GLPaint += OnPaint;

//            GuiContext.ClearCache();
//        }

//        void OnPaint(object sender, RenderEventArgs e)
//        {
//            Scene.MainCamera = e.Camera;
//            Scene.Update(e.FrameTime);

//            if (ShowBaseGrid)
//                _baseGrid.Render(e.Camera, RenderPass.Both);

//            if (ShowSkybox && SkyboxScene != null)
//            {
//                _skyboxCamera.CopyFrom(e.Camera);
//                _skyboxCamera.SetLocation(e.Camera.Location - SkyboxOrigin);
//                _skyboxCamera.SetScale(SkyboxScale);

//                SkyboxScene.MainCamera = _skyboxCamera;
//                SkyboxScene.Update(e.FrameTime);
//                SkyboxScene.RenderWithCamera(_skyboxCamera);

//                GL.Clear(ClearBufferMask.DepthBufferBit);
//            }

//            Scene.RenderWithCamera(e.Camera, _lockedCullFrustum);

//            if (_showStaticOctree)
//                _staticOctreeRenderer.Render(e.Camera, RenderPass.Both);

//            if (_showDynamicOctree)
//                _dynamicOctreeRenderer.Render(e.Camera, RenderPass.Both);
//        }

//        protected void AddRenderModeSelectionControl()
//        {
//            if (_renderModeComboBox == null)
//                _renderModeComboBox = AddSelection("Render Mode", (renderMode, _) =>
//                {
//                    foreach (var node in Scene.AllNodes)
//                        node.SetRenderMode(renderMode);

//                    if (SkyboxScene != null)
//                        foreach (var node in SkyboxScene.AllNodes)
//                            node.SetRenderMode(renderMode);
//                });
//        }

//        void SetAvailableRenderModes(IEnumerable<string> renderModes)
//        {
//            _renderModeComboBox.Items.Clear();
//            if (renderModes.Any())
//            {
//                _renderModeComboBox.Enabled = true;
//                _renderModeComboBox.Items.Add("Default Render Mode");
//                _renderModeComboBox.Items.AddRange(renderModes.ToArray());
//                _renderModeComboBox.SelectedIndex = 0;
//            }
//            else
//            {
//                _renderModeComboBox.Items.Add("(no render modes available)");
//                _renderModeComboBox.SelectedIndex = 0;
//                _renderModeComboBox.Enabled = false;
//            }
//        }

//        protected void SetEnabledLayers(HashSet<string> layers)
//        {
//            Scene.SetEnabledLayers(layers);
//            _staticOctreeRenderer = new OctreeDebugRenderer<SceneNode>(Scene.StaticOctree, Scene.GuiContext, false);
//        }
//    }
//}
