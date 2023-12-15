using UnityEngine;

namespace Common.Abstract_Bases.Visual.Settings
{
    public interface IVisualSettings
    {
        public AnimationClip EmptyActionAnimation { get; }
        public AnimationClip EmptyPrepareContinuousActionAnimation { get; }
        public AnimationClip EmptyContinuousActionAnimation { get; }
    }
}