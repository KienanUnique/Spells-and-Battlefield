using System;
using UnityEngine;

namespace Player.Visual.Settings
{
    [Serializable]
    public class PlayerVisualSettingsSection : IPlayerVisualSettings
    {
        [SerializeField] private AnimationClip _emptyUseSpellAnimation;
        public AnimationClip EmptyUseSpellAnimation => _emptyUseSpellAnimation;
    }
}