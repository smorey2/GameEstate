using GameEstate.Core;
using GameEstate.Graphics;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static GameEstate.EstateDebug;

namespace GameEstate.Formats.Tes
{
    /// <summary>
    /// Manages loading and instantiation of NIF models.
    /// </summary>
    public class NifManager
    {
        readonly EstatePakFile _pakFile;
        readonly MaterialManager _materialManager;
        GameObject _prefabContainerObj;
        readonly Dictionary<string, Task<object>> _preloadTasks = new Dictionary<string, Task<object>>();
        readonly Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
        readonly int _markerLayer;

        public NifManager(EstatePakFile pakFile, MaterialManager materialManager, int markerLayer)
        {
            _pakFile = pakFile;
            _materialManager = materialManager;
            _markerLayer = markerLayer;
        }

        public GameObject CreateObject(string filePath)
        {
            EnsurePrefabContainerObjectExists();
            // Load & cache the NIF prefab.
            if (!_prefabs.TryGetValue(filePath, out var prefab))
                prefab = _prefabs[filePath] = LoadPrefabDontAddToPrefabCache(filePath);
            // Instantiate the prefab.
            return Object.Instantiate(prefab);
        }

        public void PreloadObject(string filePath)
        {
            // If the NIF prefab has already been created we don't have to load the file again.
            if (_prefabs.ContainsKey(filePath))
                return;
            // Start loading the NIF asynchronously if we haven't already started.
            if (!_preloadTasks.TryGetValue(filePath, out _))
                _preloadTasks[filePath] = _pakFile.LoadFileObjectAsync<object>(filePath);
        }

        void EnsurePrefabContainerObjectExists()
        {
            if (_prefabContainerObj == null)
            {
                _prefabContainerObj = new GameObject("NIF Prefabs");
                _prefabContainerObj.SetActive(false);
            }
        }

        GameObject LoadPrefabDontAddToPrefabCache(string filePath)
        {
            Assert(!_prefabs.ContainsKey(filePath));
            PreloadObject(filePath);
            var file = (NiFile)_preloadTasks[filePath].Result;
            _preloadTasks.Remove(filePath);
            // Start pre-loading all the NIF's textures.
            foreach (var texturePath in file.GetTexturePaths())
                _materialManager.TextureManager.PreloadTexture(texturePath);
            var objBuilder = new NifObjectBuilder(file, _materialManager, _markerLayer);
            var prefab = objBuilder.BuildObject();
            prefab.transform.parent = _prefabContainerObj.transform;
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