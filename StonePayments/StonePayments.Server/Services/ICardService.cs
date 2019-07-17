using StonePayments.Business;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StonePayments.Server.Services
{
    public interface ICardService
    {
        Task Insert(CardModel CardModel);

        Task Update(CardModel CardModel);

        Task<List<CardModel>> GetCards(string id = "");

        Task Delete(string id);
    }
}
