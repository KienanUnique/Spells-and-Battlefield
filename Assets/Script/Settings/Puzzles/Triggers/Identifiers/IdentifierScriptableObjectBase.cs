using UnityEngine;

namespace Settings.Puzzles.Triggers.Identifiers
{
    public abstract class IdentifierScriptableObjectBase : ScriptableObject, IIdentifier
    {
        public abstract bool IsObjectOfRequiredType(Component objectToCheck);
    }
}