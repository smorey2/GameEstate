using GameEstate.Formats.Tes;
using UnityEngine;

namespace GameEstate.Graphics.ObjectBuilders
{
    public class UnityObjectBuilder : AbstractObjectBuilder<GameObject, Material, Texture2D>
    {
        GameObject _prefabObject;
        readonly int _markerLayer;

        public UnityObjectBuilder(int markerLayer) : base()
        {
            _markerLayer = markerLayer;
        }

        public override void EnsurePrefabContainerExists()
        {
            if (_prefabObject == null)
            {
                _prefabObject = new GameObject("_Prefabs");
                _prefabObject.SetActive(false);
            }
        }

        public override GameObject CreateObject(GameObject prefab) => GameObject.Instantiate(prefab);

        public override GameObject BuildObject(object source, MaterialManager<Material, Texture2D> materialManager)
        {
            var file = (NiFile)source;
            // Start pre-loading all the NIF's textures.
            foreach (var texturePath in file.GetTexturePaths())
                materialManager.TextureManager.PreloadTexture(texturePath);
            var objBuilder = new NifObjectBuilder(file, materialManager, _markerLayer);
            var prefab = objBuilder.BuildObject();
            prefab.transform.parent = _prefabObject.transform;
            // Add LOD support to the prefab.
            var LODComponent = prefab.AddComponent<LODGroup>();
            var LODs = new LOD[1]
            {
                new LOD(0.015f, prefab.GetComponentsInChildren<Renderer>())
            };
            LODComponent.SetLODs(LODs);
            return prefab;
        }
    }
}