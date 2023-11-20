using System;
using Common.Animation_Data;
using Common.Animation_Data.Continuous_Action;

namespace Player.Spell_Manager.Spell_Handlers
{
    public interface IPlayerSpellsHandler
    {
        public event Action<IContinuousActionAnimationData> NeedPlayContinuousActionAnimation;
        public event Action<IAnimationData> NeedPlaySingleActionAnimation;
        public event Action SpellCanceled;
        public event Action SpellCasted;
        public event Action SpellHandled;
        public bool IsBusy { get; }
        public bool TryInterrupt();
        public void OnSpellCastPartOfAnimationFinished();
    }
}