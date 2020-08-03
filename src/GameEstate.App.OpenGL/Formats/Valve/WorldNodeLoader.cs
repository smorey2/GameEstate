using GameEstate.Formats.Valve.Blocks;
using GameEstate.Graphics.OpenGL;
using System.Collections.Generic;
using System;
using System.Numerics;
using GameEstate.Graphics;

namespace GameEstate.Formats.Valve
{
    public class WorldNodeLoader
    {
        readonly DATAWorldNode _node;
        readonly IGLContext _context;

        public WorldNodeLoader(IGLContext context, DATAWorldNode node)
        {
            _node = node;
            _context = context;
        }

        public void Load(Scene scene)
        {
            var data = _node.Data;

            var worldLayers = data.ContainsKey("m_layerNames") ? data.Get<string[]>("m_layerNames") : Array.Empty<string>();
            var sceneObjectLayerIndices = data.ContainsKey("m_sceneObjectLayerIndices") ? data.GetIntArray("m_sceneObjectLayerIndices") : null;
            var sceneObjects = data.GetArray("m_sceneObjects");
            var i = 0;

            // Output is WorldNode_t we need to iterate m_sceneObjects inside it
            foreach (var sceneObject in sceneObjects)
            {
                var layerIndex = sceneObjectLayerIndices?[i++] ?? -1;

                // sceneObject is SceneObject_t
                var renderableModel = sceneObject.Get<string>("m_renderableModel");
                var matrix = sceneObject.GetArray("m_vTransform").ToMatrix4x4();

                var tintColorWrongVector = sceneObject.GetSub("m_vTintColor").ToVector4();
                var tintColor = tintColorWrongVector.W == 0
                    ? Vector4.One // Ignoring tintColor, it will fuck things up.
                    : new Vector4(tintColorWrongVector.X, tintColorWrongVector.Y, tintColorWrongVector.Z, tintColorWrongVector.W);

                if (renderableModel != null)
                {
                    var newResource = _context.LoadFile<BinaryPak>($"{renderableModel}_c");
                    if (newResource == null)
                        continue;
                    var modelNode = new ModelSceneNode(scene, (DATAModel)newResource.DATA, null, false)
                    {
                        Transform = matrix,
                        Tint = tintColor,
                        LayerName = worldLayers[layerIndex],
                    };
                    scene.Add(modelNode, false);
                }

                var renderable = sceneObject.Get<string>("m_renderable");
                if (renderable != null)
                {
                    var newResource = _context.LoadFile<BinaryPak>($"{renderable}_c");
                    if (newResource == null)
                        continue;
                    var meshNode = new MeshSceneNode(scene, new DATAMesh(newResource))
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