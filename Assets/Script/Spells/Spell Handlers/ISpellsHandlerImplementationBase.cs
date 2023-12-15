using System;
using Common.Abstract_Bases.Disableable;
using Common.Animation_Data;
using Common.Animation_Data.Continuous_Action;

namespace Spells.Spell_Handlers
{
    public interface ISpellsHandlerImplementationBase : IDisableable
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