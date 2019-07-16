using System;

namespace StonePayments.Util
{
    /// <summary>
    /// Interface base para todas as entidades do domínio do problema (classes de negócio).
    /// </summary>
    public interface IBaseEntity
    {
        Guid Id { get; set; }

        bool Equals(object obj);

        int GetHashCode();
    }
}