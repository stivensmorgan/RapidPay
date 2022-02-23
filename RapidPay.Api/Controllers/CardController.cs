using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Entities.DTO;
using RapidPay.Entities.Interfaces;
using RapidPay.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardController : ControllerBase
    {
        private readonly ICardManagementService cardService;
        public CardController(ICardManagementService _cardService) =>
            cardService = _cardService;


        [HttpGet]
        [Route("GetBalance")]
        public async Task<IActionResult> GetBalance(int id)
        {
            try
            {
                var card = await cardService.GetCardBalance(id);

                return Ok(card);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("CreateCard")]
        public async Task<IActionResult> CreateCard(CreateCardDTO card)
        {
            try
            {
                var newCard = await cardService.CreateCard(card);
                return Ok(newCard);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Pay")]
        public async Task<IActionResult> Pay(NewPaymentDTO payment)
        {
            try
            {
                await cardService.Pay(payment);
                return Ok("Payment Successed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
