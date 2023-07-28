using System.Collections.Generic;
using Common.Animation_Data;
using Common.Mechanic_Effects;

namespace Enemies.State_Machine.States.Concrete_Types.Melee_Attack
{
    public interface IMeleeAttackStateData
    {
        AnimationData AnimationData { get; }
        IReadOnlyCollection<IMechanicEffect> HitMechanicEffects { get; }
    }
}