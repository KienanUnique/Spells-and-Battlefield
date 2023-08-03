using UnityEngine;

namespace Common.Animation_Data
{
    [CreateAssetMenu(fileName = "Animation Data",
        menuName = ScriptableObjectsMenuDirectories.RootDirectory + "Animation Data", order = 0)]
    public class AnimationData : ScriptableObject, IAnimationData
    {
        [SerializeField] private AnimationClip _animation;
        [Min(0.001f)] [SerializeField] private float _animationSpeed = 1;

        public float AnimationSpeed => _animationSpeed;
        public AnimationClip Clip => _animation;
    }
}