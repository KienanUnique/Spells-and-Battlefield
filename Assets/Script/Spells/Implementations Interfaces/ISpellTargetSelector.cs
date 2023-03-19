using System.Collections.Generic;
using Interfaces;

namespace Spells.Implementations_Interfaces
{
    public interface ISpellTargetSelector : ISpellImplementation
    {
        public List<ISpellInteractable> SelectTargets();
    }
}