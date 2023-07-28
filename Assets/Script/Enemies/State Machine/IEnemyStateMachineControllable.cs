using System;
using System.Collections.Generic;
using Common.Animation_Data;
using Common.Mechanic_Effects;
using Common.Readonly_Transform;
using Enemies.Target_Selector_From_Triggers;
using Interfaces;

namespace Enemies.State_Machine
{
    public interface IEnemyStateMachineControllable
    {
        public event Action AnimationUseActionMomentTrigger;
        public IEnemyTargetFromTriggersSelector TargetFromTriggersSelector { get; }
        public void StartFollowingObject(IReadonlyTransform target);
        public void StopFollowingObject();
        public void StartPlayingActionAnimation(IAnimationData animationData);
        public void StopPlayingActionAnimation();
        public void ApplyEffectsToTargets(IReadOnlyCollection<IEnemyTarget> targets,
            IReadOnlyCollection<IMechanicEffect> mechanicEffects);
    }
}