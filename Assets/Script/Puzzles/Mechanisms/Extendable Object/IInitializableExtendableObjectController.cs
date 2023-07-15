using System.Collections.Generic;
using Puzzles.Mechanisms_Triggers;
using Settings.Puzzles.Mechanisms;
using UnityEngine;

namespace Puzzles.Mechanisms.Extendable_Object
{
    public interface IInitializableExtendableObjectController
    {
        public void Initialize(List<IMechanismsTrigger> triggers, ExtendableObjectState startState,
            Vector3 startPosition, Vector3 endPosition, float animationDuration, Transform objectToExtend,
            ExtendableObjectsSettings settings);
    }
}