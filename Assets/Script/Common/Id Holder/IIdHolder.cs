using System;

namespace Common.Id_Holder
{
    public interface IIdHolder : IEquatable<IIdHolder>
    {
        public int Id { get; }
    }
}