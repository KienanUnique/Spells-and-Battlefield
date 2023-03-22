using Interfaces;
using UnityEngine;

namespace Spells
{
    public abstract class SpellBase : ScriptableObject, ISpell
    {
        public Sprite Icon => _icon;
        public string Title => _title;
        public abstract AnimatorOverrideController CastAnimationAnimatorOverrideController { get; }
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _title;

        public abstract void Cast(Vector3 spawnSpellPosition, Quaternion spawnSpellRotation, Transform casterTransform,
            ISpellInteractable casterCharacter);
    }
}