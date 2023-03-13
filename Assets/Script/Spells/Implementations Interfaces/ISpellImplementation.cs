using UnityEngine;

public interface ISpellImplementation
{
#nullable enable
    public void Initialize(Rigidbody spellRigidbody, Transform? castObjectTransform, ICharacter casterCharacter);
#nullable disable
}