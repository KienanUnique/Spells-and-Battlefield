using UnityEngine;

public abstract class SpellImplementationBase : ISpellImplementation
{
    protected Rigidbody _spellRigidbody;
    protected Transform _casterTransform;
    protected ISpellInteractable _casterInterface;
    public virtual void Initialize(Rigidbody spellRigidbody, Transform casterTransform, ISpellInteractable casterInterface)
    {
        _spellRigidbody = spellRigidbody;
        _casterTransform = casterTransform;
        _casterInterface = casterInterface;
    }
}