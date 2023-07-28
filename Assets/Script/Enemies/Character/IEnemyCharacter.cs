using System.Collections.Generic;
using Common.Abstract_Bases.Character;
using Common.Mechanic_Effects;
using Interfaces;

namespace Enemies.Character
{
    public interface IEnemyCharacter : ICharacterBase
    {
        public void ApplyEffectsToTargets(IReadOnlyCollection<IEnemyTarget> targets,
            IReadOnlyCollection<IMechanicEffect> mechanicEffects);
    }
}