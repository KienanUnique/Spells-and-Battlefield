using UnityEngine;

namespace Common.Animation_Data
{
    public interface IAnimationData
    {
        public float AnimationSpeed { get; }
        public AnimatorOverrideController AnimationAnimatorOverrideController { get; }
    }
}