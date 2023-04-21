using Spells;

namespace Interfaces
{
    public interface ISpellInteractable : ICharacter, IInteractable
    {
        public void ApplyContinuousEffect(IContinuousEffect effect);
    }
}