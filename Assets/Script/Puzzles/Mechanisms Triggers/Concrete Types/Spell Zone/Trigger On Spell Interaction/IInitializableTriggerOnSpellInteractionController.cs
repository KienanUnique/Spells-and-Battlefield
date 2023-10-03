using Common.Id_Holder;
using Common.Readonly_Transform;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Spell_Zone.Trigger_On_Spell_Interaction
{
    public interface IInitializableTriggerOnSpellInteractionController
    {
        public void Initialize(IIdHolder idHolder, IReadonlyTransform mainTransform);
    }
}