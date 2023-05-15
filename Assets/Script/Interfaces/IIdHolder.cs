using System;

namespace Interfaces
{
    public interface IIdHolder : IComparable
    {
        public int Id { get; }
    }
}