using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Identifiers
{
    public abstract class IdentifierScriptableObjectBaseWithImplementation<TTypeToSearch>
        : IdentifierScriptableObjectBase
    {
        public override bool IsObjectOfRequiredType(Component objectToCheck)
        {
            return objectToCheck.TryGetComponent<TTypeToSearch>(out _);
        }
    }
}