using System.Collections.Generic;
using Interfaces;
using Spells.Implementations_Interfaces;

namespace Spells.Abstract_Types.Implementation_Bases
{
    public abstract class SpellTargetSelectorImplementationBase : SpellImplementationBase, ISpellTargetSelector
    {
        public abstract List<ISpellInteractable> SelectTargets();
    }
}