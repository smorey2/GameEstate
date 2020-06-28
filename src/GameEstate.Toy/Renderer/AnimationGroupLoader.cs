//using GameEstate.Toy.Models.Animations;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;

//namespace GameEstate.Toy.Renderer
//{
//    public static class AnimationGroupLoader
//    {
//        public static IEnumerable<Animation> LoadAnimationGroup(Resource resource, GuiContext guiContext)
//        {
//            var data = GetData(resource);

//            // Get the list of animation files
//            var animArray = data.GetArray<string>("m_localHAnimArray").Where(a => a != null);
//            // Get the key to decode the animations
//            var decodeKey = data.GetSubCollection("m_decodeKey");

//            var animationList = new List<Animation>();

//            // Load animation files
//            foreach (var animationFile in animArray)
//                animationList.AddRange(LoadAnimationFile(animationFile, decodeKey, guiContext));

//            return animationList;
//        }

//        public static IEnumerable<Animation> TryLoadSingleAnimationFileFromGroup(Resource resource, string animationName, GuiContext guiContext)
//        {
//            var data = GetData(resource);

//            // Get the list of animation files
//            var animArray = data.GetArray<string>("m_localHAnimArray").Where(a => a != null);
//            // Get the key to decode the animations
//            var decodeKey = data.GetSubCollection("m_decodeKey");

//            var animation = animArray.FirstOrDefault(a => a != null && a.EndsWith($"{animationName}.vanim"));

//            return animation != default ? LoadAnimationFile(animation, decodeKey, guiContext) : (IEnumerable<Animation>)null;
//        }

//        static IKeyValueCollection GetData(Resource resource) => resource.DataBlock is NTRO ntro
//           ? ntro.Output as IKeyValueCollection
//           : ((BinaryKV3)resource.DataBlock).Data;

//        static IEnumerable<Animation> LoadAnimationFile(string animationFile, IKeyValueCollection decodeKey, GuiContext guiContext)
//        {
//            var animResource = guiContext.LoadFileByAnyMeansNecessary(animationFile + "_c");

//            if (animResource == null)
//                throw new FileNotFoundException($"Failed to load {animationFile}_c. Did you configure game paths correctly?");

//            // Build animation classes
//            return Animation.FromResource(animResource, decodeKey);
//        }
//    }
//}
