using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Identifiers
{
    public interface IIdentifier
    {
        public bool IsObjectOfRequiredType(Component objectToCheck);
    }
}