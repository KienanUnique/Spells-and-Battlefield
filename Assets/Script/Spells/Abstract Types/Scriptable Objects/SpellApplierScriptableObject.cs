using UnityEngine;

public abstract class SpellApplierScriptableObject : ScriptableObject
{
    public abstract ISpellApplier GetImplementationObject();
}
