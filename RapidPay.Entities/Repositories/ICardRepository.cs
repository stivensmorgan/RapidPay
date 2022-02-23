using RapidPay.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.Entities.Interfaces
{
    public interface ICardRepository
    {
        void CreateCard(Card card);

        void UpdateCard(Card card);

        Card GetCardById(int id);

        Card GetCardByNumber(long cardNumber);
    }
}
