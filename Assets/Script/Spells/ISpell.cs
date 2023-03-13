using UnityEngine;

public interface ISpell
{
    public AnimatorOverrideController CastAnimationAnimatorOverrideController { get; }
    public void Cast(Vector3 spawnSpellPosition, Quaternion spawnSpellRotation, Transform casterTransform, ISpellInteractable casterCharacter);
}