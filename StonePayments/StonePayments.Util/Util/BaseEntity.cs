using System;

namespace StonePayments.Util
{
    /// <summary>
    /// Classe mãe para entidades do modelo de domínio.
    /// </summary>
    public class BaseEntity: IBaseEntity
    {
        public Guid Id { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Id.Equals(obj);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
