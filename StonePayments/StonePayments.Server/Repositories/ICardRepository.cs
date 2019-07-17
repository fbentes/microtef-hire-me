using StonePayments.Business;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StonePayments.Server.Repositories
{
    public interface ICardRepository
    {
        Task Insert(CardModel cardModel);

        Task Update(CardModel cardModel);

        Task<List<CardModel>> GetCards(string id = "");

        Task Delete(string id);
    }
}
