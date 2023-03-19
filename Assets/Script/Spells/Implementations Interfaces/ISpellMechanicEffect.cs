using System.Collections.Generic;
using Interfaces;

namespace Spells.Implementations_Interfaces
{
    public interface ISpellMechanicEffect : ISpellImplementation
    {
        public void ApplyEffectToTargets(List<ISpellInteractable> targets);
    }
}