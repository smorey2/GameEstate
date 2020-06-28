using System.Collections.Generic;
using System.Numerics;

namespace GameEstate.Toy.Models.Animations
{
    public class Bone
    {
        public Bone(string name, List<int> index, Vector3 position, Quaternion rotation)
        {
            Parent = null;
            Children = new List<Bone>();

            Name = name;
            SkinIndices = index;

            Position = position;
            Angle = rotation;

            // Calculate matrices
            BindPose = Matrix4x4.CreateFromQuaternion(rotation) * Matrix4x4.CreateTranslation(position);

            Matrix4x4.Invert(BindPose, out var inverseBindPose);
            InverseBindPose = inverseBindPose;
        }

        public Bone Parent { get; private set; }
        public List<Bone> Children { get; }

        public string Name { get; }
        public List<int> SkinIndices { get; }

        public Vector3 Position { get; }
        public Quaternion Angle { get; }

        public Matrix4x4 BindPose { get; }
        public Matrix4x4 InverseBindPose { get; }

        public void AddChild(Bone child) => Children.Add(child);

        public void SetParent(Bone parent) => Parent = parent;
    }
}
