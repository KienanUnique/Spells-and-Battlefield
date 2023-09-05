using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Identifiers
{
    public abstract class IdentifierScriptableObjectBase : ScriptableObject, IIdentifier
    {
        public abstract bool IsObjectOfRequiredType(Component objectToCheck);
    }
}