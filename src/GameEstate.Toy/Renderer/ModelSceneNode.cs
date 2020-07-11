//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Numerics;
//using GameEstate.Toy.Blocks;
//using GameEstate.Toy.Models;
//using GameEstate.Toy.Models.Animations;
//using OpenTK.Graphics.OpenGL;

//namespace GameEstate.Toy.Renderer
//{
//    public class ModelSceneNode : SceneNode, IRenderableMeshCollection
//    {
//        Model Model { get; }

//        public Vector4 Tint
//        {
//            get => meshRenderers.Count > 0 ? meshRenderers[0].Tint : Vector4.One;
//            set { foreach (var renderer in meshRenderers) renderer.Tint = value; }
//        }

//        public IEnumerable<RenderableMesh> RenderableMeshes => _activeMeshRenderers;

//        readonly List<RenderableMesh> meshRenderers = new List<RenderableMesh>();
//        readonly List<Animation> animations = new List<Animation>();
//        Dictionary<string, string> skinMaterials;

//        Animation _activeAnimation;
//        int _animationTexture;
//        Skeleton _skeleton;
//        ICollection<string> _activeMeshGroups = new HashSet<string>();
//        ICollection<RenderableMesh> _activeMeshRenderers = new HashSet<RenderableMesh>();

//        float _time;

//        public ModelSceneNode(Scene scene, Model model, string skin = null, bool loadAnimations = true)
//            : base(scene)
//        {
//            Model = model;

//            // Load required resources
//            if (loadAnimations)
//            {
//                LoadSkeleton();
//                LoadAnimations();
//            }

//            if (skin != null)
//                SetSkin(skin);

//            LoadMeshes();
//            UpdateBoundingBox();
//        }

//        public override void Update(Scene.UpdateContext context)
//        {
//            if (_activeAnimation == null)
//                return;

//            // Update animation matrices
//            var animationMatrices = new float[_skeleton.AnimationTextureSize * 16];
//            for (var i = 0; i < _skeleton.AnimationTextureSize; i++)
//            {
//                // Default to identity matrices
//                animationMatrices[i * 16] = 1.0f;
//                animationMatrices[(i * 16) + 5] = 1.0f;
//                animationMatrices[(i * 16) + 10] = 1.0f;
//                animationMatrices[(i * 16) + 15] = 1.0f;
//            }

//            _time += context.Timestep;
//            animationMatrices = _activeAnimation.GetAnimationMatricesAsArray(_time, _skeleton);

//            // Update animation texture
//            GL.BindTexture(TextureTarget.Texture2D, _animationTexture);
//            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba32f, 4, _skeleton.AnimationTextureSize, 0, PixelFormat.Rgba, PixelType.Float, animationMatrices);
//            GL.BindTexture(TextureTarget.Texture2D, 0);
//        }

//        public override void Render(Scene.RenderContext context) { } // This node does not render itself; it uses the batching system via IRenderableMeshCollection

//        public override IEnumerable<string> GetSupportedRenderModes() => meshRenderers.SelectMany(renderer => renderer.GetSupportedRenderModes()).Distinct();

//        public override void SetRenderMode(string renderMode)
//        {
//            foreach (var renderer in meshRenderers)
//                renderer.SetRenderMode(renderMode);
//        }

//        void SetSkin(string skin)
//        {
//            var materialGroups = Model.Get<IDictionary<string, object>[]>("m_materialGroups");
//            string[] defaultMaterials = null;

//            foreach (var materialGroup in materialGroups)
//            {
//                // "The first item needs to match the default materials on the model"
//                if (defaultMaterials == null)
//                    defaultMaterials = materialGroup.Get<string[]>("m_materials");

//                if (materialGroup.Get<string>("m_name") == skin)
//                {
//                    var materials = materialGroup.Get<string[]>("m_materials");

//                    skinMaterials = new Dictionary<string, string>();

//                    for (var i = 0; i < defaultMaterials.Length; i++)
//                        skinMaterials[defaultMaterials[i]] = materials[i];

//                    break;
//                }
//            }
//        }

//        void LoadMeshes()
//        {
//            // Get embedded meshes
//            foreach (var embeddedMesh in Model.GetEmbeddedMeshesAndLoD().Where(m => (m.LoDMask & 1) != 0))
//                meshRenderers.Add(new RenderableMesh(embeddedMesh.Mesh, Scene.GuiContext, skinMaterials));

//            // Load referred meshes from file (only load meshes with LoD 1)
//            var referredMeshesAndLoDs = Model.GetReferenceMeshNamesAndLoD();
//            foreach (var refMesh in referredMeshesAndLoDs.Where(m => (m.LoDMask & 1) != 0))
//            {
//                var newResource = Scene.GuiContext.LoadFileByAnyMeansNecessary(refMesh.MeshName + "_c");
//                if (newResource == null)
//                    continue;

//                if (!newResource.ContainsBlockType(BlockType.VBIB))
//                {
//                    Console.WriteLine("Old style model, no VBIB!");
//                    continue;
//                }

//                meshRenderers.Add(new RenderableMesh(new Mesh(newResource), Scene.GuiContext, skinMaterials));
//            }

//            // Set active meshes to default
//            SetActiveMeshGroups(Model.GetDefaultMeshGroups());
//        }

//        void LoadSkeleton() => _skeleton = Model.GetSkeleton();

//        void SetupAnimationTexture()
//        {
//            if (_animationTexture == default)
//            {
//                // Create animation texture
//                _animationTexture = GL.GenTexture();
//                GL.BindTexture(TextureTarget.Texture2D, _animationTexture);
//                // Set clamping to edges
//                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
//                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
//                // Set nearest-neighbor sampling since we don't want to interpolate matrix rows
//                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
//                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);
//                //Unbind texture again
//                GL.BindTexture(TextureTarget.Texture2D, 0);
//            }
//        }

//        void LoadAnimations()
//        {
//            var animGroupPaths = Model.GetReferencedAnimationGroupNames();
//            var emebeddedAnims = Model.GetEmbeddedAnimations();

//            if (!animGroupPaths.Any() && !emebeddedAnims.Any())
//                return;

//            SetupAnimationTexture();

//            // Load animations from referenced animation groups
//            foreach (var animGroupPath in animGroupPaths)
//            {
//                var animGroup = Scene.GuiContext.LoadFileByAnyMeansNecessary(animGroupPath + "_c");
//                animations.AddRange(AnimationGroupLoader.LoadAnimationGroup(animGroup, Scene.GuiContext));
//            }

//            // Get embedded animations
//            animations.AddRange(emebeddedAnims);
//        }

//        public void LoadAnimation(string animationName)
//        {
//            var animGroupPaths = Model.GetReferencedAnimationGroupNames();
//            var embeddedAnims = Model.GetEmbeddedAnimations();

//            if (!animGroupPaths.Any() && !embeddedAnims.Any())
//                return;

//            if (_skeleton == default)
//            {
//                LoadSkeleton();
//                SetupAnimationTexture();
//            }

//            // Get embedded animations
//            var embeddedAnim = embeddedAnims.FirstOrDefault(a => a.Name == animationName);
//            if (embeddedAnim != default)
//            {
//                animations.Add(embeddedAnim);
//                return;
//            }

//            // Load animations from referenced animation groups
//            foreach (var animGroupPath in animGroupPaths)
//            {
//                var animGroup = Scene.GuiContext.LoadFileByAnyMeansNecessary(animGroupPath + "_c");
//                var foundAnimations = AnimationGroupLoader.TryLoadSingleAnimationFileFromGroup(animGroup, animationName, Scene.GuiContext);
//                if (foundAnimations != default)
//                {
//                    animations.AddRange(foundAnimations);
//                    return;
//                }
//            }
//        }

//        public IEnumerable<string> GetSupportedAnimationNames() => animations.Select(a => a.Name);

//        public void SetAnimation(string animationName)
//        {
//            _time = 0f;
//            _activeAnimation = animations.FirstOrDefault(a => a.Name == animationName);

//            if (_activeAnimation != default)
//                foreach (var renderer in meshRenderers)
//                    renderer.SetAnimationTexture(_animationTexture, _skeleton.AnimationTextureSize);
//            else
//                foreach (var renderer in meshRenderers)
//                    renderer.SetAnimationTexture(null, 0);
//        }

//        public IEnumerable<string> GetMeshGroups() => Model.GetMeshGroups();

//        public ICollection<string> GetActiveMeshGroups() => _activeMeshGroups;

//        public void SetActiveMeshGroups(IEnumerable<string> meshGroups)
//        {
//            _activeMeshGroups = new HashSet<string>(GetMeshGroups().Intersect(meshGroups));

//            var groups = GetMeshGroups();
//            if (groups.Count() > 1)
//            {
//                _activeMeshRenderers.Clear();
//                foreach (var group in _activeMeshGroups)
//                {
//                    var meshMask = Model.GetActiveMeshMaskForGroup(group).ToArray();
//                    for (var meshIndex = 0; meshIndex < meshRenderers.Count; meshIndex++)
//                        if (meshMask[meshIndex] && !_activeMeshRenderers.Contains(meshRenderers[meshIndex]))
//                            _activeMeshRenderers.Add(meshRenderers[meshIndex]);
//                }
//            }
//            else
//                _activeMeshRenderers = new HashSet<RenderableMesh>(meshRenderers);
//        }

//        void UpdateBoundingBox()
//        {
//            var first = true;
//            foreach (var mesh in meshRenderers)
//            {
//                LocalBoundingBox = first ? mesh.BoundingBox : BoundingBox.Union(mesh.BoundingBox);
//                first = false;
//            }
//        }
//    }
//}
