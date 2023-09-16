using System.Collections.Generic;
using Common.Mechanic_Effects;

namespace Enemies.Character
{
    public interface IEnemyTargetsEffectsApplier
    {
        public void ApplyEffectsToTargets(IReadOnlyCollection<IEnemyTarget> targets,
            IReadOnlyCollection<IMechanicEffect> mechanicEffects);
    }
}