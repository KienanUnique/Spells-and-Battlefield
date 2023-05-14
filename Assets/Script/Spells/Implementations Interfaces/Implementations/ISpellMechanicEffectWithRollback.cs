namespace Spells.Implementations_Interfaces.Implementations
{
    public interface ISpellMechanicEffectWithRollback : ISpellMechanicEffect
    {
        public void Rollback();
    }
}