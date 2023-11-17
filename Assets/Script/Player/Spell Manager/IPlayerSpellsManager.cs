using System;
using Common.Animation_Data;
using Player.Spell_Manager.Spells_Selector;

namespace Player.Spell_Manager
{
    public interface IPlayerSpellsManager : IPlayerSpellsManagerInformation, IPlayerSpellsSelector
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