using System.Collections.Generic;
using Interfaces;

namespace Spells.Implementations_Interfaces.Implementations
{
    public interface ISpellTargetSelector : ISpellImplementation
    {
        public List<ISpellInteractable> SelectTargets();
    }
}