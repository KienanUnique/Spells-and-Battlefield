using System;

namespace Puzzles.Mechanisms_Triggers
{
    public interface IMechanismsTrigger
    {
        public event Action Triggered;
    }
}