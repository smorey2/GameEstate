using GameEstate.Toy.Models;
using System;
using System.Numerics;

namespace GameEstate.Toy.Renderer
{
    public class WorldNodeLoader
    {
        readonly WorldNode _node;
        readonly GuiContext _guiContext;

        public WorldNodeLoader(GuiContext vrfGuiContext, WorldNode node)
        {
            _node = node;
            _guiContext = vrfGuiContext;
        }

        public void Load(Scene scene)
        {
            var data = _node.Data;

            var worldLayers = data.ContainsKey("m_layerNames") ? data.GetArray<string>("m_layerNames") : Array.Empty<string>();
            var sceneObjectLayerIndices = data.ContainsKey("m_sceneObjectLayerIndices") ? data.GetIntegerArray("m_sceneObjectLayerIndices") : null;
            var sceneObjects = data.GetArray("m_sceneObjects");
            var i = 0;

            // Output is WorldNode_t we need to iterate m_sceneObjects inside it
            foreach (var sceneObject in sceneObjects)
            {
                var layerIndex = sceneObjectLayerIndices?[i++] ?? -1;

                // sceneObject is SceneObject_t
                var renderableModel = sceneObject.GetProperty<string>("m_renderableModel");
                var matrix = sceneObject.GetArray("m_vTransform").ToMatrix4x4();

                var tintColorWrongVector = sceneObject.GetSubCollection("m_vTintColor").ToVector4();

                var tintColor = tintColorWrongVector.W == 0
                    ? Vector4.One // Ignoring tintColor, it will fuck things up.
                    : new Vector4(tintColorWrongVector.X, tintColorWrongVector.Y, tintColorWrongVector.Z, tintColorWrongVector.W);

                if (renderableModel != null)
                {
                    var newResource = _guiContext.LoadFileByAnyMeansNecessary(renderableModel + "_c");

                    if (newResource == null)
                        continue;

                    var modelNode = new ModelSceneNode(scene, (Model)newResource.DataBlock, null, false)
                    {
                        Transform = matrix,
                        Tint = tintColor,
                        LayerName = worldLayers[layerIndex],
                    };

                    scene.Add(modelNode, false);
                }

                var renderable = sceneObject.GetProperty<string>("m_renderable");

                if (renderable != null)
                {
                    var newResource = _guiContext.LoadFileByAnyMeansNecessary(renderable + "_c");

                    if (newResource == null)
                        continue;

                    var meshNode = new MeshSceneNode(scene, new Mesh(newResource))
                    {
                        Transform = matrix,
                        Tint = tintColor,
                        LayerName = worldLayers[layerIndex],
                    };

                    scene.Add(meshNode, false);
                }
            }
        }
    }
}
