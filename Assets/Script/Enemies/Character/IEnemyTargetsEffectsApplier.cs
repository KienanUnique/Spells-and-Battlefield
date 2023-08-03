using System.Collections.Generic;
using Common.Mechanic_Effects;
using Interfaces;

namespace Enemies.Character
{
    public interface IEnemyTargetsEffectsApplier
    {
        public void ApplyEffectsToTargets(IReadOnlyCollection<IEnemyTarget> targets,
            IReadOnlyCollection<IMechanicEffect> mechanicEffects);
    }
}