using System.Collections.Generic;
using System.Linq;
using Spells.Abstract_Types.Implementation_Bases;
using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Concrete_Types.Mechanics
{
    [CreateAssetMenu(fileName = "Continuous Mechanic",
        menuName = "Spells and Battlefield/Spell System/Mechanic/Continuous Mechanic", order = 0)]
    public class ContinuousMechanic : SpellContinuousMechanicEffectScriptableObject
    {
        [SerializeField] private List<SpellMechanicEffectScriptableObject> _mechanicEffects;
        [SerializeField] private float _cooldownInSeconds;
        [SerializeField] private float _durationInSeconds;
        [SerializeField] private bool _needIgnoreCooldown;

        public override ISpellMechanicEffect GetImplementationObject()
        {
            var spellMechanicEffects = new List<ISpellMechanicEffect>();
            _mechanicEffects.ForEach(effectScriptableObject =>
                spellMechanicEffects.Add(effectScriptableObject.GetImplementationObject()));
            return new ContinuousMechanicImplementation(new ContinuousEffect(_cooldownInSeconds, spellMechanicEffects,
                _durationInSeconds, _needIgnoreCooldown));
        }

        private class ContinuousMechanicImplementation : SpellContinuousMechanicEffectImplementationBase
        {
            public ContinuousMechanicImplementation(IContinuousEffect effect) : base(effect)
            {
            }
        }
    }
}