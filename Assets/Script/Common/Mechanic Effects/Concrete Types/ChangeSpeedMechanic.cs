using System.Collections.Generic;
using Common.Mechanic_Effects.Scriptable_Objects;
using Interfaces;
using UnityEngine;

namespace Common.Mechanic_Effects.Concrete_Types
{
    [CreateAssetMenu(fileName = "Change Speed Mechanic",
        menuName = ScriptableObjectsMenuDirectories.MechanicsDirectory + "Change Speed Mechanic", order = 0)]
    public class ChangeSpeedMechanic : MechanicEffectScriptableObject
    {
        [Min(0.0001f)] [SerializeField] private float _changeSpeedRatio;

        public override IMechanicEffect GetImplementationObject()
        {
            return new ChangeSpeedMechanicImplementation(_changeSpeedRatio);
        }

        private class ChangeSpeedMechanicImplementation : InstantMechanicEffectImplementationBase,
            IMechanicEffectWithRollback
        {
            private readonly float _changeSpeedRatio;
            private readonly List<IMovable> _affectedTargets = new List<IMovable>();

            public ChangeSpeedMechanicImplementation(float changeSpeedRatio)
            {
                _changeSpeedRatio = changeSpeedRatio;
            }

            public override void ApplyEffectToTarget(IInteractable target)
            {
                if (target.TryGetComponent(out IMovable movable))
                {
                    movable.MultiplySpeedRatioBy(_changeSpeedRatio);
                    _affectedTargets.Add(movable);
                }
            }

            public void Rollback()
            {
                foreach (var movable in _affectedTargets)
                {
                    movable?.DivideSpeedRatioBy(_changeSpeedRatio);
                }
            }
        }
    }
}