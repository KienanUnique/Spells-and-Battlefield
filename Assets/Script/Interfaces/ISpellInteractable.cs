using Spells;

namespace Interfaces
{
    public interface ISpellInteractable : ICharacter, IInteractable
    {
        void ApplyContinuousEffect(IContinuousEffect effect);
    }
}