using System.Collections.Generic;

namespace Spells.Implementations_Interfaces.Implementations
{
    public interface ISpellTargetSelector : ISpellImplementation
    {
        public IReadOnlyList<ISpellInteractable> SelectTargets();
    }
}