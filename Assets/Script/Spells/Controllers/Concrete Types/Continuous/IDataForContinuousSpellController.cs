using Spells.Controllers.Data_For_Controller;

namespace Spells.Controllers.Concrete_Types.Continuous
{
    public interface IDataForContinuousSpellController : IDataForSpellController
    {
        public float DurationInSeconds { get; }
    }
}