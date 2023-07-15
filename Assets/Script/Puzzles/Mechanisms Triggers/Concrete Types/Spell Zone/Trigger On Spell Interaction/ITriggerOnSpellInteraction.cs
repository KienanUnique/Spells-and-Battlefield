using System;
using Spells.Implementations_Interfaces.Implementations;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Spell_Zone.Trigger_On_Spell_Interaction
{
    public interface ITriggerOnSpellInteraction
    {
        event Action<ISpellType> SpellTypeInteractionDetected;
    }
}