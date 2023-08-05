using System.Collections.Generic;
using Common.Animation_Data;
using Common.Mechanic_Effects;
using Enemies.Movement.Enemy_Data_For_Moving;

namespace Enemies.State_Machine.States.Concrete_Types.Melee_Attack
{
    public interface IMeleeAttackStateData
    {
        public IEnemyDataForMoving DataForMoving { get; }
        public AnimationData AnimationData { get; }
        public IReadOnlyCollection<IMechanicEffect> HitMechanicEffects { get; }
    }
}