using AutoMapper;
using CustomerService.DTOs;
using CustomerService.Models;
using CustomerService.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace CustomerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReturnsController : ControllerBase
    {
        private readonly IReturnTableRepository _repository;
        private readonly IMapper _mapper;

        public ReturnsController(IReturnTableRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/ReturnTable/ByCustomer/5
        [HttpGet("ByCustomer/{customerId}")]
        public async Task<ActionResult<IEnumerable<ReturnTableDto>>> GetReturnsByCustomer(int customerId)
        {
            var returns = await _repository.GetReturnsByCustomerIdAsync(customerId);
            var returnDtos = _mapper.Map<IEnumerable<ReturnTableDto>>(returns);
            return Ok(returnDtos);
        }

        // GET: api/ReturnTable/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReturnTableDto>> GetReturn(int id)
        {
            var returnTable = await _repository.GetReturnByIdAsync(id);
            if (returnTable == null)
            {
                return NotFound();
            }
            var returnDto = _mapper.Map<ReturnTableDto>(returnTable);
            return Ok(returnDto);
        }

        // POST: api/ReturnTable
        [HttpPost]
        public async Task<ActionResult<ReturnTableDto>> CreateReturn(ReturnTableDto returnDto)
        {
            var returnTable = _mapper.Map<ReturnTable>(returnDto);
            await _repository.AddReturnAsync(returnTable);
            var createdReturnDto = _mapper.Map<ReturnTableDto>(returnTable);
            return CreatedAtAction(nameof(GetReturn), new { id = createdReturnDto.ReturnId }, createdReturnDto);
        }

        // PUT: api/ReturnTable/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReturn(int id, ReturnTableDto returnDto)
        {
            if (id != returnDto.ReturnId)
            {
                return BadRequest();
            }

            var returnTable = _mapper.Map<ReturnTable>(returnDto);
            await _repository.UpdateReturnAsync(returnTable);
            return NoContent();
        }

        // DELETE: api/ReturnTable/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReturn(int id)
        {
            await _repository.DeleteReturnAsync(id);
            return NoContent();
        }
    }
}
