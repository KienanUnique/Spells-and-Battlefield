using System;
using UnityEngine;

namespace Player.Visual.Settings
{
    [Serializable]
    public class PlayerVisualSettingsSection : IPlayerVisualSettings
    {
        [SerializeField] private AnimationClip _emptyUseSpellAnimation;
        [SerializeField] private AnimationClip _emptyPrepareContinuousActionAnimation;
        [SerializeField] private AnimationClip _emptyContinuousActionAnimation;

        public AnimationClip EmptyActionAnimation => _emptyUseSpellAnimation;
        public AnimationClip EmptyPrepareContinuousActionAnimation => _emptyPrepareContinuousActionAnimation;
        public AnimationClip EmptyContinuousActionAnimation => _emptyContinuousActionAnimation;
    }
}