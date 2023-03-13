using UnityEngine;

public abstract class SpellMovementImplementationBase : SpellImplementationBase, ISpellMovement
{
    protected Transform _spellRigidbodyTransform;

    public override void Initialize(Rigidbody spellRigidbody, Transform fromCastObjectTransform, ISpellInteractable casterCharacter)
    {
        base.Initialize(spellRigidbody, fromCastObjectTransform, casterCharacter);
        _spellRigidbodyTransform = _spellRigidbody.transform;
    }
#nullable enable
    public abstract void UpdatePosition();
#nullable disable
}