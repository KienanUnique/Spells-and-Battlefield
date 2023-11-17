using System;

namespace Spells.Controllers.Concrete_Types.Continuous
{
    public interface IContinuousSpellController
    {
        public event Action Finished;
        public float RatioOfCompletion { get; }
        public void Interrupt();
    }
}