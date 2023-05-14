using UnityEngine;

namespace Spells.Spell.Interfaces
{
    public interface ISpellAnimationInformation
    {
        public abstract AnimatorOverrideController CastAnimationAnimatorOverrideController { get; }
    }
}