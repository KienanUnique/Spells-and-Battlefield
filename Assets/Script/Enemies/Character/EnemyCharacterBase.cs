using System.Collections.Generic;
using Common.Abstract_Bases.Character;
using Common.Interfaces;
using Common.Mechanic_Effects;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Common.Mechanic_Effects.Source;
using Common.Readonly_Transform;
using Factions;
using UnityEngine;

namespace Enemies.Character
{
    public abstract class EnemyCharacterBase : CharacterBase, IDisableableEnemyCharacter
    {
        private readonly string _name;
        private readonly IEffectSourceInformation _thisEffectSourceInformation;

        protected EnemyCharacterBase(ICoroutineStarter coroutineStarter,
            EnemyCharacterSettingsSection characterSettings, IReadonlyTransform thisTransform,
            GameObject gameObjectToLink, IFaction startFaction, ISummoner summoner = null) : base(coroutineStarter,
            characterSettings, gameObjectToLink, startFaction, summoner)
        {
            _name = characterSettings.Name;
            _thisEffectSourceInformation = new EffectSourceInformation(EffectSourceType.External, thisTransform);
        }

        public ISummoner Summoner => _summoner;

        public void ApplyEffectsToTargets(IReadOnlyCollection<IEnemyTarget> targets,
            IReadOnlyCollection<IMechanicEffect> mechanicEffects)
        {
            foreach (var effect in mechanicEffects)
            {
                effect.ApplyEffectToTargets(targets, _thisEffectSourceInformation);
            }
        }
    }
}