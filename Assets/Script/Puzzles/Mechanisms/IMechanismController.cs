using System;

namespace Puzzles.Mechanisms
{
    public interface IMechanismController
    {
        public event Action JobStarted;
        public event Action JobEnded;
    }
}