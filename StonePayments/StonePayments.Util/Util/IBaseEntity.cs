using System;

namespace StonePayments.Util
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }

        bool Equals(object obj);

        int GetHashCode();
    }
}