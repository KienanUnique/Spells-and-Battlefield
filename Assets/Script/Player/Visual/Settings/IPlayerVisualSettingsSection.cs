using UnityEngine;

namespace Player.Visual.Settings
{
    public interface IPlayerVisualSettings
    {
        public AnimationClip EmptyActionAnimation { get; }
        public AnimationClip EmptyPrepareContinuousActionAnimation { get; }
        public AnimationClip EmptyContinuousActionAnimation { get; }
    }
}