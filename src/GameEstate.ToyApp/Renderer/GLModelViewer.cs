//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace GameEstate.Toy.Renderer
//{
//    /// <summary>
//    /// GL Render control with model controls (render mode, animation panels).
//    /// </summary>
//    public class GLModelViewer : GLSceneViewer
//    {
//        readonly Model _model;
//        readonly Mesh _mesh;
//        ComboBox _animationComboBox;
//        CheckedListBox _meshGroupListBox;
//        ModelSceneNode _modelSceneNode;
//        MeshSceneNode _meshSceneNode;

//        public GLModelViewer(GuiContext guiContext, Model model)
//            : base(guiContext, Frustum.CreateEmpty())
//        {
//            _model = model;
//        }

//        public GLModelViewer(GuiContext guiContext, Mesh mesh)
//           : base(guiContext, Frustum.CreateEmpty())
//        {
//            _mesh = mesh;
//        }

//        protected override void InitializeControl()
//        {
//            AddRenderModeSelectionControl();

//            _animationComboBox = ViewerControl.AddSelection("Animation", (animation, _) =>
//            {
//                _modelSceneNode?.SetAnimation(animation);
//            });
//        }

//        protected override void LoadScene()
//        {
//            if (_model != null)
//            {
//                _modelSceneNode = new ModelSceneNode(Scene, _model);
//                SetAvailableAnimations(_modelSceneNode.GetSupportedAnimationNames());
//                Scene.Add(_modelSceneNode, false);

//                var meshGroups = _modelSceneNode.GetMeshGroups();

//                if (meshGroups.Count() > 1)
//                {
//                    _meshGroupListBox = ViewerControl.AddMultiSelection("Mesh Group", selectedGroups =>
//                    {
//                        _modelSceneNode.SetActiveMeshGroups(selectedGroups);
//                    });

//                    _meshGroupListBox.Items.AddRange(_modelSceneNode.GetMeshGroups().ToArray<object>());
//                    foreach (var group in _modelSceneNode.GetActiveMeshGroups())
//                        _meshGroupListBox.SetItemChecked(_meshGroupListBox.FindStringExact(group), true);
//                }
//            }
//            else
//                SetAvailableAnimations(Enumerable.Empty<string>());

//            if (_mesh != null)
//            {
//                _meshSceneNode = new MeshSceneNode(Scene, _mesh);
//                Scene.Add(_meshSceneNode, false);
//            }
//        }

//        void SetAvailableAnimations(IEnumerable<string> animations)
//        {
//            _animationComboBox.BeginUpdate();
//            _animationComboBox.Items.Clear();

//            var count = animations.Count();

//            if (count > 0)
//            {
//                _animationComboBox.Enabled = true;
//                _animationComboBox.Items.Add($"({count} animations available)");
//                _animationComboBox.Items.AddRange(animations.ToArray());
//                _animationComboBox.SelectedIndex = 0;
//            }
//            else
//            {
//                _animationComboBox.Items.Add("(no animations available)");
//                _animationComboBox.SelectedIndex = 0;
//                _animationComboBox.Enabled = false;
//            }

//            _animationComboBox.EndUpdate();
//        }
//    }
//}
