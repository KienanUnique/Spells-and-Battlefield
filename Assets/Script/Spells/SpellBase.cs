using UnityEngine;
public abstract class SpellBase : ScriptableObject, ISpell
{
    public abstract AnimatorOverrideController CastAnimationAnimatorOverrideController { get; }
    public abstract void Cast(Vector3 spawnSpellPosition, Quaternion spawnSpellRotation, Transform casterTransform, ISpellInteractable casterCharacter);
}