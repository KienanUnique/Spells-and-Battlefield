using System.Collections.Generic;
using Puzzles.Mechanisms.Extendable_Object.Settings;
using Puzzles.Mechanisms_Triggers;
using UnityEngine;

namespace Puzzles.Mechanisms.Extendable_Object.Setup
{
    public interface IInitializableExtendableObjectController
    {
        public void Initialize(List<IMechanismsTrigger> triggers, ExtendableObjectState startState,
            Vector3 startPosition, Vector3 endPosition, float animationDuration, Transform objectToExtend,
            IExtendableObjectsSettings settings);
    }
}