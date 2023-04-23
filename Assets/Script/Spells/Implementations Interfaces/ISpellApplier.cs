namespace Spells.Implementations_Interfaces
{
    public interface ISpellApplier : ISpellTrigger
    {
        public void HandleRollbackableEffects();
    }
}