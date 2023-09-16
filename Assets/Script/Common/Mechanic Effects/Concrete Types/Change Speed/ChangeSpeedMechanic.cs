using System.Collections.Generic;
using Common.Interfaces;
using Common.Mechanic_Effects.Scriptable_Objects;
using UnityEngine;

namespace Common.Mechanic_Effects.Concrete_Types.Change_Speed
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
            private readonly List<IMovable> _affectedTargets = new List<IMovable>();
            private readonly float _changeSpeedRatio;

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
                foreach (IMovable movable in _affectedTargets)
                {
                    movable?.DivideSpeedRatioBy(_changeSpeedRatio);
                }
            }
        }
    }
}