﻿using System.Collections.Generic;
using Common.Abstract_Bases.Character;
using Common.Mechanic_Effects;
using Interfaces;

namespace Enemies.Character
{
    public abstract class EnemyCharacterBase : CharacterBase, IDisableableEnemyCharacter
    {
        private readonly string _name;

        protected EnemyCharacterBase(ICoroutineStarter coroutineStarter,
            EnemyCharacterSettingsSection characterSettings, ISummoner summoner = null) : base(coroutineStarter,
            characterSettings, summoner)
        {
            _name = characterSettings.Name;
        }

        public void ApplyEffectsToTargets(IReadOnlyCollection<IEnemyTarget> targets,
            IReadOnlyCollection<IMechanicEffect> mechanicEffects)
        {
            foreach (IMechanicEffect effect in mechanicEffects)
            {
                effect.ApplyEffectToTargets(targets);
            }
        }
    }
}