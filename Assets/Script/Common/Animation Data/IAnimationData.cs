using UnityEngine;

namespace Common.Animation_Data
{
    public interface IAnimationData
    {
        public float AnimationSpeed { get; }
        public AnimationClip Clip { get; }
    }
}