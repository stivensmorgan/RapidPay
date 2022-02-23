using RapidPay.Entities.DTO;
using RapidPay.Entities.Interfaces;
using RapidPay.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.Services
{
    public class CardManagementService : ICardManagementService
    {
        const int CARD_FORMAT = 15;
        readonly IUnitOfWork unitOfWork;
        readonly IUniversalFeeExchangeService ufeService;
        public CardManagementService(IUnitOfWork _unitOfWork, IUniversalFeeExchangeService _ufeService) =>
            (this.unitOfWork, ufeService) = (_unitOfWork, _ufeService);
        public async Task<CardDTO> CreateCard(CreateCardDTO card)
        {
            var existsCard = unitOfWork.CardRepository.GetCardByNumber(card.CardNumber);

            if (card.CardNumber.ToString().Length != CARD_FORMAT)
            {
                throw new ArgumentException("Card number " + card.CardNumber.ToString() + " not valid. " + CARD_FORMAT.ToString() + " digits expected");
            }

            if (card.Balance <= 0)
            {
                throw new ArgumentException("Card balance must be grater than zero ");
            }

            if (existsCard != null)
            {
                throw new ArgumentException("The Card " + card.CardNumber.ToString() + " already exist");
            }

            Card newCard = new()
            {
                CardNumber = card.CardNumber,
                Balance = card.Balance
            };

            unitOfWork.CardRepository.CreateCard(newCard);
            _ = await unitOfWork.SaveChanges();

            return new CardDTO()
            {
                Id = newCard.Id,
                CardNumber = newCard.CardNumber,
                Balance = newCard.Balance
            };
        }

        public Task<CardDTO> GetCardBalance(int id)
        {
            var card = unitOfWork.CardRepository.GetCardById(id);

            if (card == null)
            {
                throw new ArgumentException("The Card Id " + id.ToString() + " was not found");
            }

            return Task.FromResult(
                new CardDTO()
                {
                    Id = card.Id,
                    CardNumber = card.CardNumber,
                    Balance = card.Balance
                });
        }

        public async Task Pay(NewPaymentDTO payment)
        {
            var card = unitOfWork.CardRepository.GetCardById(payment.IdCard);

            if (card == null)
            {
                throw new ArgumentException("The Card Id " + payment.IdCard.ToString() + " was not found");
            }

            decimal fee = ufeService.GetFee();
            decimal amountWithFee = Math.Round(payment.Amount + payment.Amount * (fee / 100), 2);

            if (amountWithFee > card.Balance)
            {
                throw new ArgumentException("Insufficient funds. Balance:" + card.Balance.ToString() +
                    " Amount:" + payment.Amount.ToString() + " Fee:" + fee.ToString() +
                    " AmountWithFee:" + amountWithFee.ToString());
            }

            Payment newPayment = new()
            {
                IdCard = card.Id,
                PreviusBalance = card.Balance,
                Amount = payment.Amount,
                Fee = fee,
                DateOfPayment = DateTimeOffset.Now,
                Balance = Math.Round(card.Balance - amountWithFee, 2)
            };

            Math.Round(card.Balance -= amountWithFee, 2);

            unitOfWork.CardRepository.UpdateCard(card);
            unitOfWork.PaymentRepository.Add(newPayment);
            _ = await unitOfWork.SaveChanges();
        }
    }
}
