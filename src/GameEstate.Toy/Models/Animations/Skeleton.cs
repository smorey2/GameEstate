using System;
using System.Collections.Generic;

namespace GameEstate.Toy.Models.Animations
{
    public class Skeleton
    {
        public List<Bone> Roots { get; private set; } = new List<Bone>();
        public Bone[] Bones { get; private set; } = Array.Empty<Bone>();
        public int AnimationTextureSize { get; } = 0;

        /// <summary>
        /// Find all skeleton roots (bones without a parent).
        /// </summary>
        public void FindRoots()
        {
            // Create an empty root list
            Roots = new List<Bone>();

            foreach (var bone in Bones)
                if (bone != null && bone.Parent == null)
                    Roots.Add(bone);
        }
    }
}
