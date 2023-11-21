using System;
using Common.Animation_Data;
using Common.Animation_Data.Continuous_Action;

namespace Common.Abstract_Bases.Spells_Manager
{
    public interface ISpellManager
    {
        public event Action<IAnimationData> NeedPlaySingleActionAnimation;
        public event Action<IContinuousActionAnimationData> NeedPlayContinuousActionAnimation;
        public event Action NeedCancelActionAnimations;
        public void StartCasting();
        public void OnSpellCastPartOfAnimationFinished();
        public void OnAnimatorReadyForNextAnimation();
        public void StopCasting();
    }
}