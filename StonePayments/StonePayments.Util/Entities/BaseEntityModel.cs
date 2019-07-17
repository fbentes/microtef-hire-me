using System;

namespace StonePayments.Util
{
    /// <summary>
    /// Classe mãe para entidades do modelo de domínio.
    /// </summary>
    public class BaseEntityModel: IBaseEntityModel
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

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
