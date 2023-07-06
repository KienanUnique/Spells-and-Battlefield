using UnityEngine;

namespace Settings.Puzzles.Triggers.Identifiers
{
    public interface IIdentifier
    {
        public bool IsObjectOfRequiredType(Component objectToCheck);
    }
}