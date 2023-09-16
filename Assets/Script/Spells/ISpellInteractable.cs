using Common.Interfaces;
using Spells.Implementations_Interfaces.Implementations;

namespace Spells
{
    public interface ISpellInteractable : IInteractable
    {
        public void InteractAsSpellType(ISpellType spellType);
    }
}