using Spells.Continuous_Effect;

namespace Interfaces
{
    public interface ISpellInteractable : ICharacter, IIdHolder
    {
        public void ApplyContinuousEffect(IAppliedContinuousEffect effect);
    }
}