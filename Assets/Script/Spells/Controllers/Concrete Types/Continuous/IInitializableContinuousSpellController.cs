namespace Spells.Controllers.Concrete_Types.Continuous
{
    public interface IInitializableContinuousSpellController : IContinuousSpellController
    {
        public void Initialize();
    }
}