using System.Collections.Generic;
using Common.Mechanic_Effects.Continuous_Effect;
using Common.Mechanic_Effects.Scriptable_Objects;
using UnityEngine;

namespace Common.Mechanic_Effects.Concrete_Types
{
    [CreateAssetMenu(fileName = "Continuous Mechanic",
        menuName = ScriptableObjectsMenuDirectories.MechanicsDirectory + "Continuous Mechanic", order = 0)]
    public class ContinuousMechanic : ContinuousMechanicEffectScriptableObject
    {
        [SerializeField] private List<MechanicEffectScriptableObject> _mechanicEffects;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _cooldownInSeconds;
        [SerializeField] private float _durationInSeconds;
        [SerializeField] private bool _needIgnoreCooldown;

        public override IMechanicEffect GetImplementationObject()
        {
            var spellMechanicEffects = new List<IMechanicEffect>();
            _mechanicEffects.ForEach(effectScriptableObject =>
                spellMechanicEffects.Add(effectScriptableObject.GetImplementationObject()));
            return new ContinuousMechanicImplementation(new ContinuousEffect(_cooldownInSeconds, spellMechanicEffects,
                _durationInSeconds, _needIgnoreCooldown, _icon));
        }

        private class ContinuousMechanicImplementation : ContinuousMechanicEffectImplementationBase
        {
            public ContinuousMechanicImplementation(IContinuousEffect effect) : base(effect)
            {
            }
        }
    }
}