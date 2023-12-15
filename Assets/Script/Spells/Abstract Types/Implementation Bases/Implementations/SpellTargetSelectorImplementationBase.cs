using System.Collections.Generic;
using Spells.Implementations_Interfaces.Implementations;

namespace Spells.Abstract_Types.Implementation_Bases.Implementations
{
    public abstract class SpellTargetSelectorImplementationBase : SpellImplementationBase, ISpellTargetSelector
    {
        public abstract IReadOnlyList<ISpellInteractable> SelectTargets();
    }
}