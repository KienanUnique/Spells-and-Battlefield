using System.Collections.Generic;
using Common.Animation_Data;
using Common.Mechanic_Effects;
using Common.Mechanic_Effects.Scriptable_Objects;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types.Melee_Attack
{
    [CreateAssetMenu(fileName = "Melee Attack State Data",
        menuName = ScriptableObjectsMenuDirectories.StatesDataDirectory + "Melee Attack State Data", order = 0)]
    public class MeleeAttackStateData : ScriptableObject, IMeleeAttackStateData
    {
        [SerializeField] private List<MechanicEffectScriptableObject> _hitMechanicEffects;
        [SerializeField] private AnimationData _animationData;

        public AnimationData AnimationData => _animationData;

        public IReadOnlyCollection<IMechanicEffect> HitMechanicEffects
        {
            get
            {
                var list = new List<IMechanicEffect>();
                _hitMechanicEffects.ForEach(effect => list.Add(effect.GetImplementationObject()));
                return list;
            }
        }
    }
}