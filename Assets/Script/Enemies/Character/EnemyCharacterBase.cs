using System.Collections.Generic;
using Common.Abstract_Bases.Character;
using Common.Mechanic_Effects;
using General_Settings_in_Scriptable_Objects.Sections;
using Interfaces;
using UnityEngine;

namespace Enemies.Character
{
    public abstract class EnemyCharacterBase : CharacterBase, IDisableableEnemyCharacter
    {
        private readonly string _name;

        protected EnemyCharacterBase(ICoroutineStarter coroutineStarter,
            EnemyCharacterSettingsSection characterSettings) :
            base(coroutineStarter, characterSettings)
        {
            _name = characterSettings.Name;
        }

        public override void HandleHeal(int countOfHealthPoints)
        {
            base.HandleHeal(countOfHealthPoints);
            Debug.Log(
                $"{_name}: Handle_Heal<{countOfHealthPoints}> --> Hp_Left<{_currentCountCountOfHitPoints.Value}>, Current_State<{_currentState.Value.ToString()}>");
        }

        public override void HandleDamage(int countOfHitPoints)
        {
            base.HandleDamage(countOfHitPoints);
            Debug.Log(
                $"{_name}: Handle_Damage<{countOfHitPoints}> --> Hp_Left<{_currentCountCountOfHitPoints.Value}>, Current_State<{_currentState.Value.ToString()}>");
        }

        public void ApplyEffectsToTargets(IReadOnlyCollection<IEnemyTarget> targets,
            IReadOnlyCollection<IMechanicEffect> mechanicEffects)
        {
            foreach (var effect in mechanicEffects)
            {
                effect.ApplyEffectToTargets(targets);
            }
        }
    }
}