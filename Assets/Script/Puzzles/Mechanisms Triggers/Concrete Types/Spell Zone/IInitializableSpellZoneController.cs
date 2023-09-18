using System.Collections.Generic;
using Puzzles.Mechanisms_Triggers.Concrete_Types.Spell_Zone.Trigger_On_Spell_Interaction;
using Spells.Implementations_Interfaces.Implementations;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Spell_Zone
{
    public interface IInitializableSpellZoneController
    {
        public void Initialize(List<ISpellType> typesToTriggerOn, bool needTriggerOneTime,
            ITriggerOnSpellInteraction triggerOnSpellInteraction);
    }
}