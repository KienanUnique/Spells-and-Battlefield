namespace Common.Mechanic_Effects
{
    public interface IMechanicEffectWithRollback : IMechanicEffect
    {
        public void Rollback();
    }
}