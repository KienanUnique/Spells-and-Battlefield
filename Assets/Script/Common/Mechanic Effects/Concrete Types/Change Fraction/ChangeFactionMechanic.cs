using System.Collections.Generic;
using Common.Interfaces;
using Common.Mechanic_Effects.Scriptable_Objects;
using Common.Mechanic_Effects.Source;
using UnityEngine;

namespace Common.Mechanic_Effects.Concrete_Types.Change_Fraction
{
    [CreateAssetMenu(fileName = "Change Faction Mechanic",
        menuName = ScriptableObjectsMenuDirectories.MechanicsDirectory + "Change Faction Mechanic", order = 0)]
    public class ChangeFactionMechanic : MechanicEffectScriptableObject
    {
        public override IMechanicEffect GetImplementationObject()
        {
            return new ChangeFactionMechanicImplementation();
        }

        private class ChangeFactionMechanicImplementation : InstantMechanicEffectImplementationBase,
            IMechanicEffectWithRollback
        {
            private readonly List<IFactionChangeable> _affectedTargets = new();

            public override void ApplyEffectToTarget(IInteractable target, IEffectSourceInformation sourceInformation)
            {
                if (!target.TryGetComponent(out IFactionChangeable factionChangeable))
                {
                    return;
                }

                factionChangeable.RevertFaction();
                _affectedTargets.Add(factionChangeable);
            }

            public void Rollback()
            {
                foreach (var affectedTarget in _affectedTargets)
                {
                    affectedTarget.ResetFactionToDefault();
                }

                _affectedTargets.Clear();
            }
        }
    }
}