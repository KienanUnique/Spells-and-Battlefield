using UnityEngine;

namespace Common.Animation_Data.Continuous_Action
{
    [CreateAssetMenu(fileName = "Continuous Action Animation Data",
        menuName = ScriptableObjectsMenuDirectories.RootDirectory + "Continuous Action Animation Data", order = 0)]
    public class ContinuousActionAnimationData : ScriptableObject, IContinuousActionAnimationData
    {
        [SerializeField] private AnimationData _prepareContinuousActionAnimation;
        [SerializeField] private AnimationData _continuousActionAnimation;

        public IAnimationData PrepareContinuousActionAnimation => _prepareContinuousActionAnimation;
        public IAnimationData ContinuousActionAnimation => _continuousActionAnimation;
    }
}