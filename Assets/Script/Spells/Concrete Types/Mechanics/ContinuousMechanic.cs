using System.Collections.Generic;
using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Continuous_Effect;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Mechanics
{
    [CreateAssetMenu(fileName = "Continuous Mechanic",
        menuName = ScriptableObjectsMenuDirectories.SpellMechanicDirectory + "Continuous Mechanic", order = 0)]
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