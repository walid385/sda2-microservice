using AutoMapper;
using CustomerService.DTOs;
using CustomerService.Models;
using CustomerService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerInfoDto>>> GetCustomers()
        {
            var customers = await _repository.GetAllCustomersAsync();
            var customerDtos = _mapper.Map<IEnumerable<CustomerInfoDto>>(customers);
            return Ok(customerDtos);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerInfoDto>> GetCustomer(int id)
        {
            var customer = await _repository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            var customerDto = _mapper.Map<CustomerInfoDto>(customer);
            return Ok(customerDto);
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<CustomerInfoDto>> CreateCustomer(CustomerCreateDto customerDto)
        {
            var customer = _mapper.Map<CustomerInfo>(customerDto);
            await _repository.AddCustomerAsync(customer);
            var createdCustomerDto = _mapper.Map<CustomerInfoDto>(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomerDto.CustomerId }, createdCustomerDto);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerInfoDto customerDto)
        {
            if (id != customerDto.CustomerId)
            {
                return BadRequest();
            }

            var customer = _mapper.Map<CustomerInfo>(customerDto);
            await _repository.UpdateCustomerAsync(customer);
            return NoContent();
        }

        [HttpPut("{id}/rewards")]
        public async Task<IActionResult> UpdateCustomerRewards(int id, [FromQuery] int points)
        {
            var customer = await _repository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            customer.Rewards += points;
            await _repository.UpdateCustomerAsync(customer);

            return Ok(new { Message = "Rewards updated successfully", Rewards = customer.Rewards });
        }


        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _repository.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
