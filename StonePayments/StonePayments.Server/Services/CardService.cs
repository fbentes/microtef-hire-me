using System.Collections.Generic;
using System.Threading.Tasks;
using LightInject;
using StonePayments.Business;
using StonePayments.Server.Repositories;
using StonePayments.Util.Services;

namespace StonePayments.Server.Services
{
    public class CardService : BaseEntityModelService<CardModel, CardException>, ICardService
    {
        [Inject]
        public ICardRepository CardRepository { get; set; }

        public async Task Insert(CardModel CardModel)
        {
            validate(CardModel);

            await CardRepository.Insert(CardModel);
        }

        public async Task Update(CardModel CardModel)
        {
            validate(CardModel);

            await CardRepository.Update(CardModel);
        }

        public async Task Delete(string id)
        {
            await CardRepository.Delete(id);
        }

        public async Task<List<CardModel>> GetCards(string id = "")
        {
            return await CardRepository.GetCards(id);
        }
    }
}