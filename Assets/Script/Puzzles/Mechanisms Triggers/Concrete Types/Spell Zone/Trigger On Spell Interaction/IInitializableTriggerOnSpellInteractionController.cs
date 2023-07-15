using Interfaces;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Spell_Zone.Trigger_On_Spell_Interaction
{
    public interface IInitializableTriggerOnSpellInteractionController
    {
        public void Initialize(IIdHolder idHolder);
    }
}