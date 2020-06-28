using System;
using System.Numerics;

namespace GameEstate.Toy.Models.Animations
{
    public class Animation
    {
        public string Name { get; private set; } = string.Empty;
        public float Fps { get; private set; } = 0;

        long FrameCount;

        Frame[] Frames = Array.Empty<Frame>();

        /// <summary>
        /// Get animation matrices as an array.
        /// </summary>
        public float[] GetAnimationMatricesAsArray(float time, Skeleton skeleton) => GetAnimationMatrices(time, skeleton).Flatten();

        /// <summary>
        /// Get the animation matrix for each bone.
        /// </summary>
        public Matrix4x4[] GetAnimationMatrices(float time, Skeleton skeleton)
        {
            // Create output array
            var matrices = new Matrix4x4[skeleton.AnimationTextureSize + 1];

            // Get bone transformations
            var transforms = GetTransformsAtTime(time);

            foreach (var root in skeleton.Roots)
                GetAnimationMatrixRecursive(root, Matrix4x4.Identity, Matrix4x4.Identity, transforms, ref matrices);

            return matrices;
        }

        /// <summary>
        /// Get animation matrix recursively.
        /// </summary>
        void GetAnimationMatrixRecursive(Bone bone, Matrix4x4 parentBindPose, Matrix4x4 parentInvBindPose, Frame transforms, ref Matrix4x4[] matrices)
        {
            // Calculate world space bind and inverse bind pose
            var bindPose = parentBindPose;
            var invBindPose = parentInvBindPose * bone.InverseBindPose;

            // Calculate transformation matrix
            var transformMatrix = Matrix4x4.Identity;
            if (transforms.Bones.ContainsKey(bone.Name))
            {
                var transform = transforms.Bones[bone.Name];
                transformMatrix = Matrix4x4.CreateFromQuaternion(transform.Angle) * Matrix4x4.CreateTranslation(transform.Position);
            }

            // Apply tranformation
            var transformed = transformMatrix * bindPose;

            // Store result
            var skinMatrix = invBindPose * transformed;
            foreach (var index in bone.SkinIndices)
                matrices[index] = skinMatrix;

            // Propagate to childen
            foreach (var child in bone.Children)
                GetAnimationMatrixRecursive(child, transformed, invBindPose, transforms, ref matrices);
        }

        /// <summary>
        /// Get the transformation matrices at a time.
        /// </summary>
        /// <param name="time">The time to get the transformation for.</param>
        Frame GetTransformsAtTime(float time)
        {
            // Create output frame
            var frame = new Frame();

            if (FrameCount == 0)
                return frame;

            // Calculate the index of the current frame
            var frameIndex = (int)(time * Fps) % FrameCount;
            var t = ((time * Fps) - frameIndex) % 1;

            // Get current and next frame
            var frame1 = Frames[frameIndex];
            var frame2 = Frames[(frameIndex + 1) % FrameCount];

            // Interpolate bone positions and angles
            foreach (var bonePair in frame1.Bones)
            {
                var position = Vector3.Lerp(frame1.Bones[bonePair.Key].Position, frame2.Bones[bonePair.Key].Position, t);
                var angle = Quaternion.Slerp(frame1.Bones[bonePair.Key].Angle, frame2.Bones[bonePair.Key].Angle, t);
                frame.Bones[bonePair.Key] = new FrameBone(position, angle);
            }

            return frame;
        }

        public override string ToString() => Name;
    }
}
