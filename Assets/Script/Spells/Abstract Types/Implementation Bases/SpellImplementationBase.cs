using UnityEngine;

public abstract class SpellImplementationBase : ISpellImplementation
{
    protected Rigidbody _spellRigidbody;
    protected Transform _fromCastObjectTransform;
    protected ICharacter _casterCharacter;
    public virtual void Initialize(Rigidbody spellRigidbody, Transform fromCastObjectTransform, ICharacter casterCharacter)
    {
        _spellRigidbody = spellRigidbody;
        _fromCastObjectTransform = fromCastObjectTransform;
        _casterCharacter = casterCharacter;
    }
}