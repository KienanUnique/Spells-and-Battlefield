namespace Spells.Implementations_Interfaces
{
    public interface ISpellMechanicEffectWithRollback : ISpellMechanicEffect
    {
        public void Rollback();
    }
}