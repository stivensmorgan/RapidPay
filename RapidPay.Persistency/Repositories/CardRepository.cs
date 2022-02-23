using Microsoft.EntityFrameworkCore;
using RapidPay.Entities.Interfaces;
using RapidPay.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.Persistency.Repositories
{
    public class CardRepository : ICardRepository
    {
        readonly RapidPayContext _context;

        public CardRepository(RapidPayContext context) =>
            _context = context;
        
        public void CreateCard(Card card)
        {
            _context.Cards.Add(card);
        }

        public void UpdateCard(Card card)
        {
            _context.Cards.Update(card);
        }

        public Card GetCardByNumber(long cardNumber)
        {
            return _context.Cards.Where(c => c.CardNumber == cardNumber).FirstOrDefault();
        }

        public Card GetCardById(int id)
        {
            return _context.Cards.Find(id);
        }

        //public decimal GetCardBalance(int cardNumber)
        //{
        //    return 0;
        //}
    }
}
