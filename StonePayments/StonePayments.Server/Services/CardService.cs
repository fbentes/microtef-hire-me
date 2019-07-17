using System.Threading.Tasks;
using LightInject;
using StonePayments.Business;
using StonePayments.Util;
using StonePayments.Util.Repositories;
using StonePayments.Util.Services;

namespace StonePayments.Server.Services
{
    /// <summary>
    /// Classe estendida apenas para a sobrescrita da propriedade BaseCRUDRepository para poder
    /// ser setada pelo LightInject.
    /// </summary>
    public class CardService : BaseCRUDServiceBridge<CardModel, CardException>
    {
        [Inject]
        public override IBaseCRUDRepository<CardModel> BaseCRUDRepository { get; set; }

        public override Task Insert(CardModel entityModel)
        {
            entityModel.Password = Cryptography.Encrypt(entityModel.Password, KeyStringCryptography.VALUE);

            return base.Insert(entityModel);
        }

        public override Task Update(CardModel entityModel)
        {
            string passwordDecrypted = Cryptography.Decrypt(entityModel.Password, KeyStringCryptography.VALUE);

            entityModel.Password = Cryptography.Encrypt(passwordDecrypted, KeyStringCryptography.VALUE);

            return base.Update(entityModel);
        }
    }
}