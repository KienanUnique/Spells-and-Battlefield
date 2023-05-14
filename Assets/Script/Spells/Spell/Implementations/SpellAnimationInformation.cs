using System;
using Spells.Spell.Interfaces;
using UnityEngine;

namespace Spells.Spell.Implementations
{
    [Serializable]
    public class SpellAnimationInformation : ISpellAnimationInformation
    {
        [SerializeField] private AnimatorOverrideController _castAnimationAnimatorOverrideController;

        public AnimatorOverrideController CastAnimationAnimatorOverrideController =>
            _castAnimationAnimatorOverrideController;
    }
}