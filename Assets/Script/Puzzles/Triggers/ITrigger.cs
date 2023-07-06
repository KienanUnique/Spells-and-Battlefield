using System;

namespace Puzzles.Triggers
{
    public interface ITrigger
    {
        public event Action Trigger;
    }
}