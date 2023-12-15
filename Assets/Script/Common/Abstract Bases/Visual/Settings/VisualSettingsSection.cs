using System;
using UnityEngine;

namespace Common.Abstract_Bases.Visual.Settings
{
    [Serializable]
    public class VisualSettingsSection : IVisualSettings
    {
        [SerializeField] private AnimationClip _emptyUseSpellAnimation;
        [SerializeField] private AnimationClip _emptyPrepareContinuousActionAnimation;
        [SerializeField] private AnimationClip _emptyContinuousActionAnimation;

        public AnimationClip EmptyActionAnimation => _emptyUseSpellAnimation;
        public AnimationClip EmptyPrepareContinuousActionAnimation => _emptyPrepareContinuousActionAnimation;
        public AnimationClip EmptyContinuousActionAnimation => _emptyContinuousActionAnimation;
    }
}