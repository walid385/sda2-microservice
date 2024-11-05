using AutoMapper;
using CustomerService.DTOs;
using CustomerService.Models;
using CustomerService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GiftCardsController : ControllerBase
    {
        private readonly IGiftCardRepository _repository;
        private readonly IMapper _mapper;

        public GiftCardsController(IGiftCardRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/GiftCard/ByCustomer/5
        [HttpGet("ByCustomer/{customerId}")]
        public async Task<ActionResult<IEnumerable<GiftCardDto>>> GetGiftCardsByCustomer(int customerId)
        {
            var giftCards = await _repository.GetGiftCardsByCustomerIdAsync(customerId);
            var giftCardDtos = _mapper.Map<IEnumerable<GiftCardDto>>(giftCards);
            return Ok(giftCardDtos);
        }

        // GET: api/GiftCard/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GiftCardDto>> GetGiftCard(int id)
        {
            var giftCard = await _repository.GetGiftCardByIdAsync(id);
            if (giftCard == null)
            {
                return NotFound();
            }
            var giftCardDto = _mapper.Map<GiftCardDto>(giftCard);
            return Ok(giftCardDto);
        }

        // POST: api/GiftCard
        [HttpPost]
        public async Task<ActionResult<GiftCardDto>> CreateGiftCard(GiftCardDto giftCardDto)
        {
            var giftCard = _mapper.Map<GiftCard>(giftCardDto);
            await _repository.AddGiftCardAsync(giftCard);
            var createdGiftCardDto = _mapper.Map<GiftCardDto>(giftCard);
            return CreatedAtAction(nameof(GetGiftCard), new { id = createdGiftCardDto.GiftCardId }, createdGiftCardDto);
        }

        // PUT: api/GiftCard/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGiftCard(int id, GiftCardDto giftCardDto)
        {
            if (id != giftCardDto.GiftCardId)
            {
                return BadRequest();
            }

            var giftCard = _mapper.Map<GiftCard>(giftCardDto);
            await _repository.UpdateGiftCardAsync(giftCard);
            return NoContent();
        }

        // DELETE: api/GiftCard/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGiftCard(int id)
        {
            await _repository.DeleteGiftCardAsync(id);
            return NoContent();
        }
    }
}
