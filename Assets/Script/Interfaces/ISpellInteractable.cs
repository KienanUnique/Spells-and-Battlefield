using Spells;
using Spells.Continuous_Effect;

namespace Interfaces
{
    public interface ISpellInteractable : ICharacter, IInteractable
    {
        public void ApplyContinuousEffect(IContinuousEffect effect);
    }
}