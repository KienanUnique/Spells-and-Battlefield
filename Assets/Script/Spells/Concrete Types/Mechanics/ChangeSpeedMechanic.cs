using System.Collections.Generic;
using Interfaces;
using Spells.Abstract_Types.Implementation_Bases;
using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Concrete_Types.Mechanics
{
    [CreateAssetMenu(fileName = "Change Speed Mechanic",
        menuName = "Spells and Battlefield/Spell System/Mechanic/Change Speed Mechanic", order = 0)]
    public class ChangeSpeedMechanic : SpellMechanicEffectScriptableObject
    {
        [Min(0.0001f)] [SerializeField] private float _changeSpeedRatio;

        public override ISpellMechanicEffect GetImplementationObject()
        {
            return new ChangeSpeedMechanicImplementation(_changeSpeedRatio);
        }

        private class ChangeSpeedMechanicImplementation : SpellInstantMechanicEffectImplementationBase,
            ISpellMechanicEffectWithRollback
        {
            private readonly float _changeSpeedRatio;
            private readonly List<IMovable> _affectedTargets = new List<IMovable>();

            public ChangeSpeedMechanicImplementation(float changeSpeedRatio)
            {
                _changeSpeedRatio = changeSpeedRatio;
            }

            public override void ApplyEffectToTarget(ISpellInteractable target)
            {
                if (target is IMovable movable)
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