namespace Spells.Implementations_Interfaces.Implementations
{
    public interface ISpellApplier : ISpellTrigger
    {
        public void HandleRollbackableEffects();
    }
}