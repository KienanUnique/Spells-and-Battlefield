using UnityEngine;

public interface ISpellImplementation
{
#nullable enable
    public void Initialize(Rigidbody spellRigidbody, Transform? casterTransform, ISpellInteractable casterCharacter);
#nullable disable
}