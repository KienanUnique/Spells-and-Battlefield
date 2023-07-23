using UnityEngine;

namespace Common.Animation_Data
{
    public interface IAnimationData
    {
        float AnimationSpeed { get; }
        AnimatorOverrideController AnimationAnimatorOverrideController { get; }
    }
}