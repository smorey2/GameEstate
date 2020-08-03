using GameEstate.Formats.Valve.Blocks;
using GameEstate.Formats.Valve.Blocks.Animation;
using GameEstate.Graphics;
using GameEstate.Graphics.OpenGL;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEstate.Formats.Valve
{
    public static class AnimationGroupLoader
    {
        static IDictionary<string, object> GetData(BinaryPak resource) => resource.DATA is DATABinaryNTRO ntro
           ? ntro.Data
           : ((DATABinaryKV3)resource.DATA).Data;

        public static IEnumerable<ModelAnimation> LoadAnimationGroup(IGraphicContext context, BinaryPak resource)
        {
            var data = GetData(resource);
            var animArray = data.Get<string[]>("m_localHAnimArray").Where(a => a != null); // Get the list of animation files
            var decodeKey = data.GetSub("m_decodeKey"); // Get the key to decode the animations

            // Load animation files
            var list = new List<ModelAnimation>();
            foreach (var animationFile in animArray)
                list.AddRange(LoadAnimationFile(context, animationFile, decodeKey));
            return list;
        }

        public static IEnumerable<ModelAnimation> TryLoadSingleAnimationFileFromGroup(IGraphicContext context, BinaryPak resource, string animationName)
        {
            var data = GetData(resource);
            var animArray = data.Get<string[]>("m_localHAnimArray").Where(a => a != null); // Get the list of animation files
            var decodeKey = data.GetSub("m_decodeKey"); // Get the key to decode the animations
            // Load animation files
            var animation = animArray.FirstOrDefault(a => a != null && a.EndsWith($"{animationName}.vanim"));
            return animation != default ? LoadAnimationFile(context, animation, decodeKey) : null;
        }

        static IEnumerable<ModelAnimation> LoadAnimationFile(IGraphicContext context, string animationFile, IDictionary<string, object> decodeKey)
        {
            var animResource = context.LoadFile<BinaryPak>($"{animationFile}_c");
            if (animResource == null)
                throw new FileNotFoundException($"Failed to load {animationFile}_c. Did you configure game paths correctly?");
            // Build animation classes
            return ModelAnimation.FromResource(animResource, decodeKey);
        }
    }
}