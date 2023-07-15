using System;

namespace Interfaces
{
    public interface IIdHolder : IEquatable<IIdHolder>
    {
        public int Id { get; }
    }
}